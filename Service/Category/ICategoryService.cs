using Joboard.DTO.Category;

namespace Joboard.Service.Category
{
    public interface ICategoryService
    {
        Task<List<Entities.Category>> GetAllCategoriesAsync();
        Task<Entities.Category> GetCategoryByIdAsync(int? id);
        Task<bool> CreateCategoryAsync(CategoryCreate_DTO categoryCreate_DTO);
        Task<bool> UpdateCategoryAsync(int? id, CategoryEdit_DTO cateogryEdit_DTO);
        Task<bool> DeleteCategoryAsync(int? cateogryId);
    }
}
