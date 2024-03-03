using System.ComponentModel.DataAnnotations;

namespace Joboard.DTO.User
{
    public class Reset_Password_DTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string NewPassword { get; set; } = string.Empty;
        [Required]
        public string Token { get; set; } = string.Empty;
    }
}
