namespace WebApi.Dtos
{
    using System.ComponentModel.DataAnnotations;
    
    public class PrescriptionCreateDto
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public long CheckupId { get; set; }
    }
}