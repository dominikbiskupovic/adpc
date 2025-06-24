namespace WebApi.Models
{
    using System.ComponentModel.DataAnnotations;
    
    public class CheckupImage
    {
        public long     Id          { get; set; }
        
        [Required]
        public long     CheckupId   { get; set; }
        
        [Required]
        [Url]
        [StringLength(2048)]
        public string   FileUrl     { get; set; } = string.Empty;
        
        [Required]
        public Checkup  Checkup     { get; set; } = null!;
    }
}