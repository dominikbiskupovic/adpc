namespace WebApi.Dtos
{
    using System.ComponentModel.DataAnnotations;
    using Models;
    using System.Text.Json.Serialization;
    
    public class CheckupCreateDto
    {
        [Required]
        public long PatientId { get; set; }

        [Required]
        public long DoctorId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string   Type { get; set; }
    }
}