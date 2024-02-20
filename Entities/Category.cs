using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Joboard.Entities
{
    public class Category : Activity
    {
            [Key]
            public int Id { get; set; }

            [Required(ErrorMessage = "Category name is required.")]
            public string  Name { get; set; } = string.Empty;

            [Required]
            public int Trending { get; set; }

            public int? ParentCategoryId { get; set; }

            [ForeignKey("ParentCategoryId")]
            public Category? ParentCategory { get; set; }
    }
}
