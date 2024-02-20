using System.ComponentModel.DataAnnotations;

namespace Joboard.DTO.Category
{
    public class CategoryCreate_DTO
    {
       [Required(ErrorMessage = "CategoryName is required.")]
       public string Name { get; set; } = string.Empty;
       public int Trending {  get; set; }
       public int? ParentCategoryId { get; set; }
       public DateTime Create_at { get; set; }
    }
}
