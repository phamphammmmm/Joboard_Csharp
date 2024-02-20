using System.ComponentModel.DataAnnotations;

namespace Joboard.Entities.Customer
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int user_id { get; set; }
        [Required]
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Location {  get; set; } = string.Empty;  
    }
}
