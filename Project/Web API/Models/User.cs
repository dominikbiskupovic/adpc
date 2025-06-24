namespace WebApi.Models
{
    using System.ComponentModel.DataAnnotations;
    
    public class User
    {
        public long     Id          { get; set; }
        
        [Required]
        [StringLength(50)]
        public string   Username    { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string   Password    { get; set; } = string.Empty;
        
        [Required]
        public bool     IsAdmin     { get; set; }
    }
}