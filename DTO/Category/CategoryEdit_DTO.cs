using System.ComponentModel.DataAnnotations;

namespace Joboard.DTO.Category
{
    public class CategoryEdit_DTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "RoleName is required.")]
        public string Name { get; set; } = string.Empty;
        public int Trending { get; set; }
        public DateTime Update_at { get; set; }
    }
}
