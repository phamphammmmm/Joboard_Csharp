using DocumentFormat.OpenXml.Office2010.Excel;
using Joboard.Context;
using Joboard.DTO.User;
using Joboard.Entities.Customer;
using Joboard.Repository;
using Joboard.Service.Email;
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
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public UserController(ApplicationDbContext context, 
                              IWebHostEnvironment webHostEnvironment, 
                              IUserService userService,
                              IUserRepository userRepository,
                              IEmailService emailService)
        {
            _context = context;
            _userService = userService;
            _userRepository = userRepository;
            _emailService = emailService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("index")]
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

        //[HttpPost("forgotpassword")]
        //public async Task<IActionResult> ForgotPassword(Forgot_Password_DTO model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userService.GetUserByEmailAsync(model.Email);
        //        if (user != null)
        //        {
        //            // Tạo mã thông báo đặt lại mật khẩu
        //            var token = await _userService.GeneratePasswordResetTokenAsync(user);

        //            // Gửi email chứa liên kết đặt lại mật khẩu đến người dùng
        //            var callbackUrl = Url.Action("ResetPassword", "User", new { userId = user.Id, token }, protocol: HttpContext.Request.Scheme);
        //            await _emailService.SendPasswordResetEmailAsync(model.Email, callbackUrl);

        //            // Trả về 200 OK nếu thành công
        //            return Ok(new { message = "An email with instructions to reset your password has been sent to your email address." });
        //        }
        //    }

        //    return BadRequest(new { message = "Email not found." });
        //}

        //[HttpPost("resetpassword")]
        //public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userService.GetUserByIdAsync(model.UserId);
        //        if (user != null)
        //        {
        //            // Đặt lại mật khẩu cho người dùng
        //            var result = await _userService.ResetPasswordAsync(user, model.Token, model.NewPassword);
        //            if (result.Succeeded)
        //            {
        //                return Ok(new { message = "Your password has been reset successfully." });
        //            }
        //            else
        //            {
        //                return BadRequest(new { message = "Failed to reset password." });
        //            }
        //        }
        //    }

        //    return BadRequest(new { message = "Invalid data." });
        //}

        [HttpPost("export")]
        public async Task<IActionResult> ExportToExcel(Entities.Customer.User user)
        {
            var customers = await _userRepository.ExportToExcel();

            using (var stream = new MemoryStream())
            {
                _userService.ExportToExcel(customers, stream);
                var fileName = $"Customers_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }


        [HttpGet("import")]
        public IActionResult ImportFromExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            try
            {
                _userService.ImportFromExcel(file);

                TempData["message"] = "Customers imported successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Error importing data: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
