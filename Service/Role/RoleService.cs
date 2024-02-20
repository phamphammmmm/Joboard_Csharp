using Joboard.DTO.Role;
using Joboard.Repository.Role;

namespace Joboard.Service.Role
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> EditRoleAsync(int? Roleid, RoleEdit_DTO roleEdit_DTO)
        {
            var roleDB = await _roleRepository.GetRoleByIdAsync(Roleid);
            if (roleDB == null)
            {
                return false;
            }

            roleDB.RoleName = roleEdit_DTO.RoleName;
            roleDB.Update_at = roleEdit_DTO.Update_at;

            await _roleRepository.UpdateRoleAsync(roleDB);

            return true;
        }

        public async Task<bool> CreateRoleAsync(RoleCreate_DTO roleCreate_DTO)
        {
            var role = new Entities.Customer.Role
            {
                RoleName = roleCreate_DTO.RoleName,
                Create_at = roleCreate_DTO.Create_at
            };

            return await _roleRepository.CreateRoleAsync(role);
        }

        public async Task<List<Entities.Customer.Role>> GetAllRolesAsync()
        {
            return await _roleRepository.GetAllRolesAsync();
        }

        public async Task<Entities.Customer.Role> GetRoleByIdAsync(int? id)
        {
            return await _roleRepository.GetRoleByIdAsync(id);
        }

        public async Task<bool> UpdateRoleAsync(int? id, RoleEdit_DTO roleEdit_DTO)
        {
            var roleDB = await _roleRepository.GetRoleByIdAsync(id);
            if (roleDB == null)
            {
                return false;
            }

            roleDB.RoleName = roleEdit_DTO.RoleName;
            roleDB.Update_at = roleEdit_DTO.Update_at;

            return await _roleRepository.UpdateRoleAsync(roleDB);
        }

        public async Task<bool> DeleteRoleAsync(int? roleId)
        {
            return await _roleRepository.DeleteRoleAsync(roleId);
        }
    }
}
