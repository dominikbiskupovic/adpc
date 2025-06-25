namespace WinForms.Services
{
    using System.Globalization;
    using CsvHelper;
    using CsvHelper.Configuration;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using Configurations;
    using Models;
    
    public class XenaScraperService
    {
        private readonly string _url;
        private readonly string _downloadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "DownloadedFiles");
        private readonly MinioStorageService _minioStorageService;
        private readonly MongoDbService _mongoDbService;

        public XenaScraperService(XenaConfig xenaConfig, MinioStorageService minioStorageService, MongoDbService mongoDbService)
        {
            _minioStorageService = minioStorageService;
            _mongoDbService = mongoDbService;
            
            _url = $"https://xenabrowser.net/datapages/?hub={xenaConfig.HubUrl}";
            
            Directory.CreateDirectory(_downloadDirectory);
        }

        [Obsolete("Obsolete")]
        public async Task ScrapeAndDownloadFilesAsync()
        {
            await _minioStorageService?.ClearBucketAsync()!;

            using IWebDriver driver = new ChromeDriver();
            await driver.Navigate().GoToUrlAsync(_url);
            await Task.Delay(5000);

            var cohortLinks = driver.FindElements(By.XPath("//a[contains(@href, 'cohort=TCGA')]"));
            var cohortUrls = cohortLinks.Select(el => el.GetAttribute("href")).ToList();

            foreach (var cohortUrl in cohortUrls)
            {
                if (cohortUrl != null)
                {
                    await driver.Navigate().GoToUrlAsync(cohortUrl);
                }
                
                await Task.Delay(5000);

                var illuminaLinks = driver.FindElements(By.XPath("//a[contains(text(), 'IlluminaHiSeq pancan normalized')]"));
                foreach (var link in illuminaLinks)
                {
                    link.Click();
                    await Task.Delay(5000);

                    var downloadLink = driver.FindElements(By.XPath("//a[contains(@href, '.gz')]")).FirstOrDefault();
                    if (downloadLink != null)
                    {
                        var fileUrl = downloadLink.GetAttribute("href");
                        var fileName = Path.GetFileName(fileUrl);
                        if (fileName != null)
                        {
                            var filePath = Path.Combine(_downloadDirectory, fileName);

                            using var client = new HttpClient();
                            if (fileUrl != null)
                            {
                                await DownloadFile(client, fileUrl, filePath);
                            }
                            
                            Console.WriteLine($"Downloaded: {filePath}");
                            await _minioStorageService.UploadFileAsync(filePath);
                        }
                    }
                }
            }
        }

        private async Task DownloadFile(HttpClient client, string fileUrl, string filePath)
        {
            client.Timeout = TimeSpan.FromMinutes(15);
            using var response = await client.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            await using var fs = new FileStream(filePath, FileMode.Create);
            await response.Content.CopyToAsync(fs);
        }
        
        [Obsolete("Obsolete")]
        public async Task ProcessFiles()
        {
            var fileUrls = await _minioStorageService?.ListClinicalFilesInMinIo()!;

            foreach (var fileUrl in fileUrls)
            {
                await using var fileStream = await _minioStorageService.GetFileFromMinIo(fileUrl);
                var geneExpressions = ParseCsvFromStream(fileStream, fileUrl);
                await _mongoDbService?.SaveGeneExpressionsToMongo(geneExpressions)!;
            }
        }
        
        private List<GeneExpression> ParseCsvFromStream(Stream stream, string fileName)
        {
            var expressions = new List<GeneExpression>();
            var relevantGenes = new HashSet<string>
            {
                "C6orf150", "CCL5", "CXCL10", "TMEM173", "CXCL9", "CXCL11", "NFKB1", "IKBKE", "IRF3", "TREX1", "ATM", "IL6", "IL8"
            };

            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = "\t",
                BadDataFound = null
            });
            
            csv.Read();
            csv.ReadHeader();
            if (csv.HeaderRecord != null)
            {
                var patientIds = csv.HeaderRecord.Skip(1).ToList();

                var patientGeneExpressions = new Dictionary<string, GeneExpression>();

                while (csv.Read())
                {
                    var geneName = csv.GetField<string>(0);
                    for (int i = 0; i < patientIds.Count; i++)
                    {
                        var patientId = patientIds[i];
                        if (!patientGeneExpressions.ContainsKey(patientId))
                        {
                            patientGeneExpressions[patientId] = new GeneExpression
                            {
                                TcgaBarcode = patientId,
                                Cohort = ExtractCancerCohortFromFileName(fileName),
                                PathwayGenes = new Dictionary<string, double>()
                            };
                        }
                        if (geneName != null && relevantGenes.Contains(geneName))
                        {
                            var geneValue = csv.GetField<double>(i + 1);
                            patientGeneExpressions[patientId].PathwayGenes[geneName] = geneValue;
                        }
                    }
                }
                expressions = patientGeneExpressions.Values.ToList();
            }

            return expressions;
        }

        private string ExtractCancerCohortFromFileName(string fileName)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            
            var cohortParts = fileNameWithoutExtension.Split('.');
            
            return cohortParts.Length >= 2 ? $"{cohortParts[0]}.{cohortParts[1]}" : "Unknown";
        }
    }
}