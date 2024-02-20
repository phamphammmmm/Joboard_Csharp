using Joboard.Context;
using Microsoft.EntityFrameworkCore;

namespace Joboard.Repository.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Entities.Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<bool> CreateCategoryAsync(Entities.Category category)
        {
            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> UpdateCategoryAsync(Entities.Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int? categoryId)
        {
            if (categoryId == null)
            {
                throw new ArgumentNullException(nameof(categoryId), "CategoryId is required");
            }

            var category = await _context.Categories.FirstOrDefaultAsync(r => r.Id == categoryId);
            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Entities.Category> GetCategoryByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Category ID is required");
            }

            var category = await _context.Categories.FirstOrDefaultAsync(r => r.Id == id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found");
            }

            return category;
        }
    }
}
