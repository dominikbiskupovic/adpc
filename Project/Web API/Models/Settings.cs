namespace WebApi.Models
{
    using System.ComponentModel.DataAnnotations;
    
    public class Settings
    {
        [Required]
        [Url]
        public string Endpoint      { get; set; } = string.Empty;
        
        [Required]
        public string AccessKey     { get; set; } = string.Empty;
        
        [Required]
        public string SecretKey     { get; set; } = string.Empty;
        
        [Required]
        public string BucketName    { get; set; } = string.Empty;
    }
}