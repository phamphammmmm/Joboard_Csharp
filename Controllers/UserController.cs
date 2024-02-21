using DocumentFormat.OpenXml.Office2010.Excel;
using Joboard.Context;
using Joboard.DTO.User;
using Joboard.Entities.Customer;
using Joboard.Service;
using Joboard.Service.User;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Numerics;

namespace Joboard.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("/api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserService _userService;

        public UserController(ApplicationDbContext context, 
                              IWebHostEnvironment webHostEnvironment, 
                              IUserService userService
                              )
        {
            _context = context;
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int? id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return StatusCode(500, new { Message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUserAsync(UserCreate_DTO userCreate_DTO)
        {
            try
            {
                var result = await _userService.CreateUserAsync(userCreate_DTO);
                if (result)
                {
                    return Ok(new { Message = "User added successfully" });
                }
                else
                {
                    return NotFound(new { Message = "User not added failuraly" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return StatusCode(500, new { Message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(int? id, UserEdit_DTO userEdit_DTO)
        {
            try
            {
                var result = await _userService.UpdateUserAsync(id, userEdit_DTO);
                if (result)
                {
                    return Ok(new { Message = "User updated successfully" });
                }
                else
                {
                    return NotFound(new { Message = "User not added failuraly" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return StatusCode(500, new { Message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int? id) 
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);
                if (result)
                {
                    return Ok(new { Message = "User is deleted successfully!!" });
                }
                else
                {
                    return NotFound(new { Message = "User not found" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return StatusCode(500, new { Message = $"An error occurred: {ex.Message}" });
            }
        }

    }
}
