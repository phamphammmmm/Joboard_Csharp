using System.ComponentModel.DataAnnotations;

namespace Joboard.DTO.Tag
{
    public class TagEdit_DTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public DateTime Update_at { get; set; }
    }
}
