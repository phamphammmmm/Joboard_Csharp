using Joboard.Entities;
using System.ComponentModel.DataAnnotations;

namespace Joboard.Entities
{
    public class Company : Activity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string ImagePath { get; set; } = string.Empty;
    }
}
