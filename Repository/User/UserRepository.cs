using Joboard.Context;
using Joboard.Entities.Customer;
using Microsoft.EntityFrameworkCore;

namespace Joboard.Repository
{
    public class UserRepositoy : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepositoy(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int? userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId), "UserId is required");
            }

            var user = await _context.Users.FirstOrDefaultAsync(r => r.Id == userId);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<User> GetUserByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "User ID is required");
            }

            var user = await _context.Users.FirstOrDefaultAsync(r => r.Id == id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found");
            }

            return user;
        }

        public async Task<List<Entities.Customer.User>> ExportToExcel()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string Email)
        {
            if (Email == null)
            {
                throw new ArgumentNullException(nameof(Email), "Email is required");
            }

            var user = await _context.Users.FirstOrDefaultAsync(r => r.Email == Email);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {Email} not found");
            }

            return user;
        }
        public List<string> GetAllEmails()
        {
            return _context.Users.Select(r => r.Email).ToList();
        }
    }
}
