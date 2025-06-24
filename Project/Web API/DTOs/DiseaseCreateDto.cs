namespace WebApi.Dtos
{
    using System.ComponentModel.DataAnnotations;
    
    public class DiseaseCreateDto
    {
        [Required]
        public long MedicalRecordId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
    }
}