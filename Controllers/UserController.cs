using Joboard.Context;
using Joboard.DTO.User;
using Joboard.Entities.Customer;
using Joboard.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Numerics;

namespace Joboard.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("/api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageService _imageService;
        private readonly PasswordHelper _passwordHelper;

        public UserController(ApplicationDbContext context, 
                              IWebHostEnvironment webHostEnvironment, 
                              IImageService imageService,
                              PasswordHelper passwordHelper
                              )
        {
            _context = context;
            _imageService = imageService;
            _passwordHelper = passwordHelper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            List<User> users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest(new { Message = "Id is required!" });
                }

                var user = await _context.Users.FirstOrDefaultAsync(r => r.Id == id);
                if (user == null)
                {
                    return NotFound(new { Message = "User not found!" });
                }

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
                if (userCreate_DTO.ImageFile == null)
                {
                    return NotFound("Không tìm thấy ảnh");
                }

                string ImagePath = await _imageService.SaveImageAsync(file: userCreate_DTO.ImageFile, folderName: "avatar");
                
                var newUser = new User
                {
                    FullName = userCreate_DTO.FullName,
                    Email = userCreate_DTO.Email,
                    Phone = userCreate_DTO.Phone,
                    ImagePath = ImagePath,
                    Birthday = userCreate_DTO.Birthday,
                    Description = userCreate_DTO.Description,
                    IsPremium = userCreate_DTO.IsPremium,
                    Create_at = userCreate_DTO.Create_at,
                    Password = _passwordHelper.HashPassword(userCreate_DTO.Password)
                };
                 
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                int user_id = newUser.Id; 
                int role_id = _context.Roles.Where(r => r.RoleName == "User").Select(r => r.RoleId).FirstOrDefault();
                var userRole = new UserRole()
                {
                    User_Id = user_id,
                    Role_Id = role_id
                };
                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync(); 

                return Ok(new { Message = "User added successfully" });
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
                if (id == null)
                {
                    return BadRequest(new { Message = "Id is required." });
                }

                if (userEdit_DTO.ImageFile == null)
                {
                    return NotFound("Không tìm thấy ảnh");
                }

                var userDB = await _context.Users.FirstOrDefaultAsync(r => r.Id == id);
                if (userDB == null)
                {
                    return NotFound(new { Message = "User not found !!" });
                }

                // Xóa ảnh cũ
                if (!string.IsNullOrEmpty(userDB.ImagePath))
                {
                    _imageService.DeleteImageAsync(userDB.ImagePath);
                }

                // Lưu ảnh mới
                string ImagePath = await _imageService.SaveImageAsync(file: userEdit_DTO.ImageFile, folderName: "avatar");

                // Cập nhật thông tin user
                userDB.FullName = userEdit_DTO.FullName;
                userDB.Email = userEdit_DTO.Email;
                userDB.Phone = userEdit_DTO.Phone;
                userDB.ImagePath = ImagePath;
                userDB.Birthday = userEdit_DTO.Birthday;
                userDB.Description = userEdit_DTO.Description;
                userDB.IsPremium = userEdit_DTO.IsPremium;
                userDB.Update_at = userEdit_DTO.Update_at;
                userDB.Password = _passwordHelper.HashPassword(userEdit_DTO.Password);

                await _context.SaveChangesAsync();
                return Ok(new { Message = "User updated successfully" });
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
                if (id == null)
                {
                    return BadRequest(new { Message = "Id is required" });
                }

                var user = await _context.Users.FirstOrDefaultAsync(r => r.Id == id);
                if (user == null)
                {
                    return NotFound(new { Message = "User not found" });
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "User is deleted successfully!!" });
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
