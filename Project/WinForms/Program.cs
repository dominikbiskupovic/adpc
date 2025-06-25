namespace WinForms
{
    using Newtonsoft.Json;
    using Configurations;
    using Services;
    
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var cfgPath = Path.Combine(Application.StartupPath, "appsettings.json");
            var cfg = JsonConvert.DeserializeObject<AppConfiguration>(File.ReadAllText(cfgPath)) ?? throw new InvalidOperationException("appsettings.json load failed");

            var minioService = new MinioStorageService(cfg.Minio);
            var mongoService = new MongoDbService(cfg.Mongo);
            var xenaService = new XenaScraperService(cfg.Xena, minioService, mongoService);
            var clinicalService = new ClinicalParserService(minioService, mongoService);

            Application.Run(new Main(mongoService, xenaService, clinicalService));
        }
    }
}