namespace WebApi.Models
{
    using System.ComponentModel.DataAnnotations;
    
    public class MedicalRecord
    {
        public long     Id          { get; set; }
        
        [Required]
        public long     PatientId   { get; set; }
        
        [Required]
        public Patient  Patient     { get; set; } = null!;
        
        [Required]
        
        public ICollection<Disease> Diseases { get; set; } = new List<Disease>();
    }
}