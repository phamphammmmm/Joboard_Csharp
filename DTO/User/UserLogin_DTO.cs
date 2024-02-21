using System.ComponentModel.DataAnnotations;

namespace Joboard.DTO.User
{
    public class UserLogin_DTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
