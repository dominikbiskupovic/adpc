namespace WebApi.Models
{
    using System.ComponentModel.DataAnnotations;
    
    public class Prescription
    {
        public long     Id          { get; set; }
        
        [Required]
        [StringLength(200)]
        public string   Name        { get; set; } = string.Empty;
        
        [Required]
        public long     CheckupId   { get; set; }
    }
}