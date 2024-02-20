namespace Joboard.Repository.Category
{
    public interface ICategoryRepository
    {
        Task<List<Entities.Category>> GetAllCategoriesAsync();
        Task<Entities.Category> GetCategoryByIdAsync(int? id);
        Task<bool> CreateCategoryAsync(Entities.Category category);
        Task<bool> UpdateCategoryAsync(Entities.Category category);
        Task<bool> DeleteCategoryAsync(int? categoryId);
    }
}
