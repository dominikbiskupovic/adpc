namespace WebApi.Dtos
{
    using System.ComponentModel.DataAnnotations;
    
    public class DoctorCreateDto
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string FullName { get; set; } = string.Empty;
    }
}