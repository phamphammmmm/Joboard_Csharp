using Joboard.DTO.Category;
using Joboard.Service.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Joboard.Controllers
{
    [Route("/api/category")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> GetAllCategorys()
        {
            var categorys = await _categoryService.GetAllCategoriesAsync();
            return Ok(categorys);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> GetCategoryById(int? id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory(CategoryCreate_DTO categoryCreate_DTO)
        {
            var result = await _categoryService.CreateCategoryAsync(categoryCreate_DTO);
            if (result)
            {
                return Ok(new { Message = "Category created successfully" });
            }
            else
            {
                return StatusCode(500, new { Message = "Failed to create category" });
            }
        }


        [HttpPost("update/{CategoryId}")]
        public async Task<IActionResult> UpdateCategory(int? CategoryId, CategoryEdit_DTO categoryEdit_DTO)
        {
            var result = await _categoryService.UpdateCategoryAsync(CategoryId, categoryEdit_DTO);
            if (result)
            {
                return Ok(new { Message = "Category updated successfully" });
            }
            else
            {
                return NotFound(new { Message = "Category not found" });
            }
        }

        [HttpDelete("delete/{CategoryId}")]
        public async Task<IActionResult> DeleteCategory(int? CategoryId)
        {
            var result = await _categoryService.DeleteCategoryAsync(CategoryId);
            if (result)
            {
                return Ok(new { Message = "Category deleted successfully" });
            }
            else
            {
                return NotFound(new { Message = "Category not found" });
            }
        }
    }
}
