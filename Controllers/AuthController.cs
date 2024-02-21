using Joboard.Context;
using Joboard.DTO.User;
using Joboard.Entities.Customer;
using Joboard.Service.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Joboard.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthService _authService;

        public AuthController(ApplicationDbContext context,
                              IAuthService authService
                              )
        {
            _context = context;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin_DTO userLogin_DTO)
        {
            try
            {
                if( userLogin_DTO == null )
                {
                    return BadRequest(new { message = "Invalid user information" });
                }
                var result = await _authService.Authenticate(userLogin_DTO);


                if (result.Success)
                {
                    return Ok(new { token = result.Token });
                }

                return Unauthorized(new { message = "Invalid credentials" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { message = "An error occurred during login" });
            }
        }
    }
}
