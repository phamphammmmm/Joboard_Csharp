using Joboard.DTO.Role;

namespace Joboard.Repository.Role
{
    public interface IRoleRepository    
    {
        Task<List<Entities.Customer.Role>> GetAllRolesAsync();
        Task<Entities.Customer.Role> GetRoleByIdAsync(int? id);
        Task<bool> CreateRoleAsync(Entities.Customer.Role role);
        Task<bool> UpdateRoleAsync(Entities.Customer.Role role);
        Task<bool> DeleteRoleAsync(int? roleId);
    }
}
