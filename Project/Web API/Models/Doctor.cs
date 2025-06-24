namespace WebApi.Models
{
    using System.ComponentModel.DataAnnotations;
    
    public class Doctor
    {
        public long Id          { get; set; }
        
        [Required]
        public long UserId      { get; set; }
        
        [Required]
        [StringLength(200)]
        public string FullName  { get; set; } = string.Empty;
    }
}