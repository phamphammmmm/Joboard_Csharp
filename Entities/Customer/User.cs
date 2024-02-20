using System.ComponentModel.DataAnnotations;

namespace Joboard.Entities.Customer
{
    /// <summary>
    /// Represents a user with all properties in the system.
    /// </summary>
    public class User : Activity
    {
        [Key]
        public int Id { get; set; }

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

        [Required(ErrorMessage = "Birthday is required.")]
        public DateTime Birthday { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = String.Empty;

        public bool IsPremium { get; set; } = false;

        public string ImagePath { get; set; } = string.Empty;

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    }
}
