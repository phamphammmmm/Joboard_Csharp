using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Joboard.DTO.User
{
    public class UserCreate_DTO 
    {
        [Required(ErrorMessage = "Full name is required.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; } = string.Empty;
        public DateTime Birthday { get; set; } = DateTime.Now;
        public string Description { get; set; } = String.Empty;
        public bool IsPremium { get; set; } = false;
        public DateTime Create_at { get; set; }
        public DateTime Update_at { get; set; }

        [NotMapped]        
        public IFormFile? ImageFile { get; set; }
    }
}
