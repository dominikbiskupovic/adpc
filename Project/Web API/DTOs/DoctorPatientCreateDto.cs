namespace WebApi.Dtos
{
    using System.ComponentModel.DataAnnotations;
    
    public class DoctorPatientCreateDto
    {
        [Required]
        public long DoctorId { get; set; }

        [Required]
        public long PatientId { get; set; }
    }
}