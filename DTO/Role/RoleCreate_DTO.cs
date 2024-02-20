using System.ComponentModel.DataAnnotations;

namespace Joboard.DTO.Role
{
    public class RoleCreate_DTO
    {
        [Required(ErrorMessage = "RoleName is required.")]
        public string RoleName { get; set; } = string.Empty;
        public DateTime Create_at { get; set; }
    }
}
