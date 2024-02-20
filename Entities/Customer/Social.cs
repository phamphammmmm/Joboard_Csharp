using System.ComponentModel.DataAnnotations;

namespace Joboard.Entities.Customer
{
    public class Social
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int User_id { get; set; }
        [Required] 
        public string Link { get; set; } = string.Empty;

    }
}
