using Joboard.DTO.User;
using Microsoft.AspNetCore.Identity;

namespace Joboard.Service.User
{
    public interface IUserService
    {
        Task<List<Entities.Customer.User>> GetAllUsersAsync();
        Task<Entities.Customer.User> GetUserByIdAsync(int? id);
        Task<bool> CreateUserAsync(UserCreate_DTO userCreate_DTO);
        Task<bool> UpdateUserAsync(int? id, UserEdit_DTO userEdit_DTO);
        Task<bool> DeleteUserAsync(int? userId);
        void ImportFromExcel(IFormFile file);
        void ExportToExcel(List<Entities.Customer.User> user, Stream stream);
        List<string> GetAllEmails();
        Task<string> GeneratePasswordResetTokenAsync(Entities.Customer.User user);
        Task<IdentityResult> ResetPasswordAsync(Entities.Customer.User user, string token, string newPassword);
    }
}
