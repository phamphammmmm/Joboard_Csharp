using System.ComponentModel.DataAnnotations;

namespace Joboard.Entities.Customer
{
    public class Role : Activity
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; } = string.Empty;
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
