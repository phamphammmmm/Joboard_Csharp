using Joboard.Entities;
using System.ComponentModel.DataAnnotations;

namespace Joboard.Entities
{
    public class Company : Activity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string Scale { get; set; } = string.Empty;

        [StringLength(255)]
        public string Avatar_Img_Path { get; set; } = string.Empty;

        [StringLength(255)]
        public string Cover_Img_Path { get; set; } = string.Empty;
    }
}
