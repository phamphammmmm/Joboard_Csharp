using System.ComponentModel.DataAnnotations;

namespace Joboard.DTO.Tag
{
    public class TagCreate_DTO
    {

        [Required(ErrorMessage = "RoleName is required.")]
        public string Name { get; set; } = string.Empty;
        public DateTime Create_at { get; set; }
    }
}
