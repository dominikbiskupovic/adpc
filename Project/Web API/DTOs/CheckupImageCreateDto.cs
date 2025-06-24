namespace WebApi.Dtos
{
    using System.ComponentModel.DataAnnotations;
    
    public class CheckupImageCreateDto
    {
        [Required]
        public long CheckupId { get; set; }

        [Required]
        [Url]
        [StringLength(2048)]
        public string FileUrl { get; set; } = string.Empty;
    }
}