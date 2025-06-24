namespace WebApi.Models
{
    using System.ComponentModel.DataAnnotations;

    public class DoctorPatient
    {
        public long Id          { get; set; }
        
        [Required]
        public long DoctorId    { get; set; }
        
        [Required]
        public long PatientId   { get; set; }
    }
}