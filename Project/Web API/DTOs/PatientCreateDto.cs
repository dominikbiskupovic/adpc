namespace WebApi.Dtos
{
    using System.ComponentModel.DataAnnotations;
    
    public class PatientCreateDto
    {
        [Required]
        [StringLength(50)]
        public string PersonalId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Surname { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public bool Sex { get; set; }
    }
}