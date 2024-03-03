using System.ComponentModel.DataAnnotations;

namespace Joboard.DTO.User
{
    public class Forgot_Password_DTO    
    {
        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
