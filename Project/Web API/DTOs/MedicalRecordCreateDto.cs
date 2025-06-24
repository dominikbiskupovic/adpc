namespace WebApi.Dtos
{
    using System.ComponentModel.DataAnnotations;
    
    public class MedicalRecordCreateDto
    {
        [Required]
        public long PatientId { get; set; }
    }
}