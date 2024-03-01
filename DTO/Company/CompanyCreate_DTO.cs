using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Joboard.DTO.Company
{
    public class CompanyCreate_DTO
    {
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

        public DateTime Created_at { get; set; }

        [NotMapped]
        public IFormFile? Avatar_Image_File { get; set; }
        [NotMapped]
        public IFormFile? Cover_Image_File { get; set; }

    }
}
