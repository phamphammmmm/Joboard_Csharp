namespace Joboard.Repository
{
    public interface IUserRepository   
    {
        Task<List<Entities.Customer.User>> GetAllUsersAsync();
        Task<Entities.Customer.User> GetUserByIdAsync(int? id);
        Task<bool> CreateUserAsync(Entities.Customer.User user);
        Task<bool> UpdateUserAsync(Entities.Customer.User user);
        Task<bool> DeleteUserAsync(int? userId);
        Task<List<Entities.Customer.User>> ExportToExcel();
        Task<Entities.Customer.User> GetUserByEmailAsync(String Email);
        List<string> GetAllEmails();
    }
}
