namespace WebApi.Dtos
{
    using System.ComponentModel.DataAnnotations;
    
    public class UserCreateDto
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public bool IsAdmin { get; set; }
    }
}