using Joboard.Context;
using Joboard.Controllers;
using Joboard.DTO.User;
using Joboard.Entities;
using Joboard.Service.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Joboard.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly PasswordHelper _passwordHelper;

        public AuthService(IConfiguration configuration, ApplicationDbContext context, PasswordHelper passwordHelper)
        {
            _context = context;
            _configuration = configuration;
            _passwordHelper = passwordHelper;
        }

        

        public async Task<AuthenticationResult> Authenticate(UserLogin_DTO userLogin_DTO)
        {
            if (userLogin_DTO == null || string.IsNullOrEmpty(userLogin_DTO.Email) || string.IsNullOrEmpty(userLogin_DTO.Password))
            {
                Console.WriteLine("Invalid user information");
                return new AuthenticationResult { Success = false, Token = null };
            }

            if (await Authentication(userLogin_DTO))
            {
                try
                {
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userLogin_DTO.Email);
                    int role_Id = await _context.UserRoles.Where(r => r.User_Id == user.Id)
                                                          .Select(r => r.Role_Id)
                                                          .FirstOrDefaultAsync();

                    var role = await _context.Roles.Where(r => r.RoleId == role_Id)
                                                      .Select(r => r.RoleName)
                                                      .FirstOrDefaultAsync();

                    // Payload
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, user.FullName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, role),
                    };

                    // Signature
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);


                    var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:ExpirationInMinutes"])),
                        signingCredentials: creds
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    // Create and return AuthenticationResult
                    return new AuthenticationResult
                    {
                        Success = true,
                        Token = tokenString
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
            }
            else
            {
                return new AuthenticationResult { Success = false, Token = null };
            }
        }

        public async Task<bool> Authentication(UserLogin_DTO userLogin_DTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userLogin_DTO.Email);

            if (user != null && _passwordHelper.VerifyPassword(userLogin_DTO.Password, user.Password))
            {
                Console.WriteLine("Authentication succeeded");
                
                return true;
            }

            Console.WriteLine("Authentication failed");
            return false;
        }
    }
}
