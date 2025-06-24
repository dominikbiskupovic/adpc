namespace WebApi.Models
{
    using System.ComponentModel.DataAnnotations;
    
    public class Disease
    {
        public long         Id              { get; set; }
        
        [Required]
        public long         MedicalRecordId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string       Name            { get; set; } = string.Empty;
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime     StartDate       { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime?    EndDate         { get; set; }
    }
}