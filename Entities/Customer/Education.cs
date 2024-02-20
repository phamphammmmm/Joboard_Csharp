using System.ComponentModel.DataAnnotations;

namespace Joboard.Entities.Customer
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        [Required] 
        public int User_id { get; set; } 
        [Required]
        [StringLength(50)]
        public string Univeristy { get; set; } = string.Empty;


    }
}
