namespace WinForms.Services
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using Models;
    using Utils;
    
    public  class ClinicalParserService
    {
        private const string MainUrl = "https://xenabrowser.net/datapages/?hub=https://tcga.xenahubs.net:443";
        
        private readonly string _downloadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Clinical");

        private readonly MinioStorageService    _minioStorageService;
        private readonly MongoDbService         _mongoDbService;

        public ClinicalParserService(MinioStorageService minioStorageService, MongoDbService mongoDbService)
        {
            _minioStorageService    = minioStorageService;
            _mongoDbService         = mongoDbService;
            
            Directory.CreateDirectory(_downloadDirectory);
        }
        
        [Obsolete("Obsolete")]
        public async Task ScrapeAndDownloadClinicalFilesAsync()
        {
            await _minioStorageService?.ClearBucketAsync()!;

            using var driver = new ChromeDriver();
            await driver.Navigate().GoToUrlAsync(MainUrl);
            await Task.Delay(5000);

            var links = driver.FindElements(By.XPath("//a[contains(@href, 'cohort=TCGA')]"))
                .Select(l => l.GetAttribute("href"))
                .ToList();

            foreach (var url in links)
            {
                await driver.Navigate().GoToUrlAsync(url);
                await Task.Delay(3000);

                var survivalData = driver.FindElements(By.XPath("//a[contains(text(), 'Curated survival data')]"));
                if (survivalData.Count == 0)
                {
                    Console.WriteLine($"No curated survival data for cohort: {url}");
                    continue;
                }

                survivalData[0].Click();
                await Task.Delay(3000);

                var txtLinks = driver.FindElements(By.XPath("//a[contains(@href, '.txt')]"));
                if (txtLinks.Count == 0)
                {
                    Console.WriteLine("No .txt download link found.");
                    driver.Navigate().Back();
                    await Task.Delay(2000);
                    continue;
                }

                var href = txtLinks[0].GetAttribute("href");
                var name = Path.GetFileName(href);
                var path = Path.Combine(_downloadDirectory, name);

                using (var http = new HttpClient())
                {
                    await DownloadFile(http, href, path);
                    await _minioStorageService.UploadFileAsync(path);
                }

                Console.WriteLine($"Downloaded and uploaded: {name}");

                driver.Navigate().Back();
                await Task.Delay(2000);
            }
        }

        public async Task MergeClinicalWithGeneExpressionAsync()
        {
            var clinicalFileNames = await _minioStorageService?.ListClinicalFilesInMinIo();
          
            var clinicalDictionaries = new List<Dictionary<string, PatientClinicalRecord>>();
            foreach (var fileName in clinicalFileNames)
            {
                var localPath = await _minioStorageService?.DownloadClinicalFileFromMinIo(_downloadDirectory, fileName);
                var dict = TsvParser.ParseClinicalData(localPath);
                clinicalDictionaries.Add(dict);
            }
            var mergedClinicalDict = new Dictionary<string, PatientClinicalRecord>();
            foreach (var kvp in clinicalDictionaries.SelectMany(dict => dict))
            {
                mergedClinicalDict[kvp.Key] = kvp.Value;
            }
            
            var allGeneExpr = await _mongoDbService.GetGeneExpressionsAsync();

            foreach (var expr in allGeneExpr)
            {
                var barcode = expr.TcgaBarcode.Trim().ToUpper();
                if (mergedClinicalDict.TryGetValue(barcode, out var clinical))
                    expr.PatientClinicalRecord = clinical;
            }
            await _mongoDbService.UpdateClinicalDataAsync(allGeneExpr);

            Console.WriteLine("Clinical data successfully merged and updated in MongoDB.");
        }

        private async Task DownloadFile(HttpClient client, string fileUrl, string filePath)
        {
            client.Timeout = TimeSpan.FromMinutes(15);
            using var response = await client.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            await using var fs = new FileStream(filePath, FileMode.Create);
            await response.Content.CopyToAsync(fs);
        }
    }
}
