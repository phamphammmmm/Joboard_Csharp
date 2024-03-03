using DocumentFormat.OpenXml.Spreadsheet;
using Joboard.Context;
using Joboard.Controllers;
using Joboard.DTO.User;
using Joboard.Entities.Customer;
using Joboard.Repository;
using Joboard.Service.User.QrCode;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Globalization;

namespace Joboard.Service.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IImageService _imageService;
        private readonly PasswordHelper _passwordHelper;
        private readonly ApplicationDbContext _context;
        private readonly IQrCodeService _qrCodeService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserService(IUserRepository customerRepository, 
                           IWebHostEnvironment webHostEnvironment,
                           IImageService imageService,
                           IQrCodeService qrCodeService,
                           ApplicationDbContext context,
                           PasswordHelper passwordHelper)
        {
            _context = context;
            _imageService = imageService;
            _qrCodeService = qrCodeService;
            _passwordHelper = passwordHelper;
            _userRepository = customerRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void ImportFromExcel(IFormFile file)
        {
            using (var package = new ExcelPackage(file.OpenReadStream()))
            {
                var worksheet = package.Workbook.Worksheets[0]; 

                int rowCount = worksheet.Dimension.Rows;

                List<Entities.Customer.User> importedCustomers = new List<Entities.Customer.User>();

                for (int row = 2; row <= rowCount; row++) 
                {
                    var newCustomer = new Entities.Customer.User
                    {
                        FullName = worksheet.Cells[row, 2].Value?.ToString(),
                        Email = worksheet.Cells[row, 3].Value?.ToString(),
                        Password = worksheet.Cells[row, 4].Value?.ToString(),
                        Phone = worksheet.Cells[row, 5].Value?.ToString(),
                        IsPremium = worksheet.Cells[row, 6].Value?.ToString() == "Yes",
                        Create_at = ParseDateTime(worksheet.Cells[row, 7].Value?.ToString()) ?? DateTime.MinValue,
                        Update_at = ParseDateTime(worksheet.Cells[row, 8].Value?.ToString()) ?? DateTime.MinValue,
                        Birthday = ParseDateTime(worksheet.Cells[row, 6].Value?.ToString()) ?? DateTime.MinValue
                    };
                    importedCustomers.Add(newCustomer);
                }

                foreach (var user in importedCustomers)
                {
                    _userRepository.CreateUserAsync(user);
                }
            }
        }

        private DateTime? ParseDateTime(string dateTimeString)
        {
            if (DateTime.TryParseExact(dateTimeString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            return null;
        }

        public void ExportToExcel(List<Entities.Customer.User> users, Stream stream)
        {
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Users");

                // Header row
                worksheet.Cells["A1"].Value = "ID";
                worksheet.Cells["B1"].Value = "Full Name";
                worksheet.Cells["C1"].Value = "Email";
                worksheet.Cells["D1"].Value = "Phone";
                worksheet.Cells["E1"].Value = "Birthday";

                // Data rows
                for (var i = 0; i < users.Count; i++)
                {
                    var row = i + 2; // Start from row 2 (1 for header)
                    var user = users[i];

                    worksheet.Cells[$"A{row}"].Value = user.Id;
                    worksheet.Cells[$"B{row}"].Value = user.FullName;
                    worksheet.Cells[$"C{row}"].Value = user.Email;
                    worksheet.Cells[$"D{row}"].Value = user.IsPremium ? "Yes" : "No";
                    worksheet.Cells[$"E{row}"].Value = user.Birthday != null ? user.Birthday.ToShortDateString() : "Unknown";
                }

                // Auto-fit columns for better readability
                worksheet.Cells.AutoFitColumns();

                // Style header row
                using (var range = worksheet.Cells["A1:E1"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }

                package.Save();
            }
        }

    public async Task<List<Entities.Customer.User>> GetAllUsersAsync()
        {
            List<Entities.Customer.User> users = await _userRepository.GetAllUsersAsync();
            return users;
        }

        public async Task<Entities.Customer.User> GetUserByIdAsync(int? id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<bool> CreateUserAsync(UserCreate_DTO userCreate_DTO)
        {

            if (userCreate_DTO.ImageFile == null)
            {
                return false;
            }

            string ImagePath = await _imageService.SaveImageAsync(file: userCreate_DTO.ImageFile, folderName: "avatar");

            var newUser = new Entities.Customer.User
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

            await _userRepository.CreateUserAsync(newUser);
            string QRcode = _qrCodeService.GenerateQRCodeUrl(newUser);
            
            int user_id = newUser.Id;
            int role_id = _context.Roles.Where(r => r.RoleName == "User").Select(r => r.RoleId).FirstOrDefault();
            var userRole = new UserRole()
            {
                User_Id = user_id,
                Role_Id = role_id
            };
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserAsync(int? id, UserEdit_DTO userEdit_DTO)
        {
            if (id == null || userEdit_DTO == null)
            {
                return false;
            }

            var userDB = await _context.Users.FirstOrDefaultAsync(r => r.Id == id);
            if (userDB == null)
            {
                return false;
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

            return true;
        }

        public async Task<bool> DeleteUserAsync(int? userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }

        Task<string> GeneratePasswordResetTokenAsync(Entities.Customer.User user)
        {
            throw new NotImplementedException();
        }

        Task<IdentityResult> ResetPasswordAsync(Entities.Customer.User user, string token, string newPassword)
        {
            throw new NotImplementedException();
        }

        Task<string> IUserService.GeneratePasswordResetTokenAsync(Entities.Customer.User user)
        {
            throw new NotImplementedException();
        }

        Task<IdentityResult> IUserService.ResetPasswordAsync(Entities.Customer.User user, string token, string newPassword)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllEmails()
        {
            return _userRepository.GetAllEmails();
        }
    }
}
