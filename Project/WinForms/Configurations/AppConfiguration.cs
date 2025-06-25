namespace WinForms.Configurations
{
    public class AppConfiguration
    {
        public MinioConfig  Minio   { get; set; }
        public MongoConfig  Mongo   { get; set; }
        public XenaConfig   Xena    { get; set; }
    }
}