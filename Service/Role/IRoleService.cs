using Joboard.DTO.Role;

namespace Joboard.Service.Role
{
    public interface IRoleService
    {
        Task<List<Entities.Customer.Role>> GetAllRolesAsync();
        Task<Entities.Customer.Role> GetRoleByIdAsync(int? id);
        Task<bool> CreateRoleAsync(RoleCreate_DTO roleCreate_DTO);
        Task<bool> UpdateRoleAsync(int? id, RoleEdit_DTO roleEdit_DTO);
        Task<bool> DeleteRoleAsync(int? roleId);
    }

}
