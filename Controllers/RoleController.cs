using Joboard.DTO.Role;
using Joboard.Service.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Joboard.Controllers
{
    [Route("/api/roles")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleService  _roleService;

        public RoleController(IRoleService  roleService)
        {
            _roleService  = roleService ;
        }

        [HttpPost]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpPost("{id}")]
        public async  Task<IActionResult> GetRoleById(int? id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            return Ok(role);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRole(RoleCreate_DTO roleCreate_DTO)
        {
            var result = await _roleService.CreateRoleAsync(roleCreate_DTO);
            if (result)
            {
                return Ok(new { Message = "Role created successfully" });
            }
            else
            {
                return StatusCode(500, new { Message = "Failed to create role" });
            }
        }


        [HttpPost("update/{RoleId}")]
        public async Task<IActionResult> UpdateRole(int? RoleId, RoleEdit_DTO roleEdit_DTO)
        {
            var result = await _roleService.UpdateRoleAsync(RoleId, roleEdit_DTO);
            if (result)
            {
                return Ok(new { Message = "Role updated successfully" });
            }
            else
            {
                return NotFound(new { Message = "Role not found" });
            }
        }

        [HttpDelete("delete/{RoleId}")]
        public async Task<IActionResult> DeleteRole(int? RoleId)
        {
            var result = await _roleService.DeleteRoleAsync(RoleId);
            if (result)
            {
                return Ok(new { Message = "Role deleted successfully" });
            }
            else
            {
                return NotFound(new { Message = "Role not found" });
            }
        }
    }
}
