namespace WinForms.Configurations
{
    public class MinioConfig
    {
        public string   Endpoint    { get; set; }
        public int      Port        { get; set; }
        public bool     UseSsl      { get; set; }
        public string   AccessKey   { get; set; }
        public string   SecretKey   { get; set; }
        public string   BucketName  { get; set; }
    }
}