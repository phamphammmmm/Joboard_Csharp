using System.ComponentModel.DataAnnotations;

namespace Joboard.DTO.Role
{
    public class RoleEdit_DTO
    {
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; } = string.Empty;
        public DateTime Update_at { get; set; }
    }
}
