﻿using Joboard.DTO.User;

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
        void ExportExcel(List<Entities.Customer.User> user, Stream stream);
    }
}
