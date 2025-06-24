namespace WebApi.Models
{
    using System.ComponentModel.DataAnnotations;
    
    public class Checkup
    {
        public long             Id          { get; set; }
        
        [Required]
        public long             PatientId   { get; set; }
        
        [Required]
        public long             DoctorId    { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime         Date        { get; set; }
        
        [Required]
        public string           Type        { get; set; }
        
        [Required]
        public ICollection<CheckupImage> Images { get; set; } = new List<CheckupImage>();
    }
}