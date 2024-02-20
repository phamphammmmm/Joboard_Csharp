using Joboard.Entities;
using System.ComponentModel.DataAnnotations;

namespace Joboard.Entities
{
    public class Tag : Activity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
