using Joboard.DTO.Category;
using Joboard.Repository.Category;
using Joboard.Service.Category;

namespace Joboard.Service.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> EditCategoryAsync(int? Categoryid, CategoryEdit_DTO categoryEdit_DTO)
        {
            var categoryDB = await _categoryRepository.GetCategoryByIdAsync(Categoryid);
            if (categoryDB == null)
            {
                return false;
            }

            categoryDB.Name = categoryEdit_DTO.Name;
            categoryDB.Update_at = categoryEdit_DTO.Update_at;

            await _categoryRepository.UpdateCategoryAsync(categoryDB);

            return true;
        }

        public async Task<bool> CreateCategoryAsync(CategoryCreate_DTO categoryCreate_DTO)
        {
            var category = new Entities.Category
            {
                Name = categoryCreate_DTO.Name,
                Trending = categoryCreate_DTO.Trending,
                Create_at = categoryCreate_DTO.Create_at
            };

            if (categoryCreate_DTO.ParentCategoryId == null)
            {
                category.ParentCategory = null;
            }
            else
            {
                var parentCategory = await _categoryRepository.GetCategoryByIdAsync(categoryCreate_DTO.ParentCategoryId);

                if (parentCategory == null)
                {
                    return false;
                }

                category.ParentCategory = parentCategory;
            }

            return await _categoryRepository.CreateCategoryAsync(category);
        }


        public async Task<List<Entities.Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<Entities.Category> GetCategoryByIdAsync(int? id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task<bool> UpdateCategoryAsync(int? id, CategoryEdit_DTO categoryEdit_DTO)
        {
            var categoryDB = await _categoryRepository.GetCategoryByIdAsync(id);
            if (categoryDB == null)
            {
                return false;
            }

            categoryDB.Name = categoryEdit_DTO.Name;
            categoryDB.Update_at = categoryEdit_DTO.Update_at;

            return await _categoryRepository.UpdateCategoryAsync(categoryDB);
        }

        public async Task<bool> DeleteCategoryAsync(int? categoryId)
        {
            return await _categoryRepository.DeleteCategoryAsync(categoryId);
        }
    }
}
