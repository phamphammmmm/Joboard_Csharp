using Joboard.Entities.Customer;

namespace Joboard.Entities
{
    public class User_Role
    {
        public int UserId { get; set; }
        public required User User { get; set; }

        public int RoleId { get; set; } 
        public required Role Role { get; set; }
    }
}
