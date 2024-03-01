using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Joboard.Entities;
using Joboard.Entities.Customer;

namespace Joboard.Entities
{
    public class Job : Activity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public bool IsPremium { get; set; }
        [Required]
        public string Salary { get; set; } = string.Empty;
        [Required]
        public string Location { get; set; } = string.Empty;
        [Required]
        public bool isActive { get; set; } = false;
        [Required]
        public string TagIds { get; set; } = String.Empty;
        [Required]
        public DateTime Deadline { get; set; } = DateTime.Now;
        [Required]
        public string Requirements { get; set; } = string.Empty;
        [Required]
        public string Level { get; set; } = string.Empty;
        [Required]
        public string EXP { get; set; } = string.Empty;
        [Required]
        public bool isRemote { get; set; } = false;
        [Required]
        public int NumberOfVacancies { get; set; }
        [Required]
        public string Type { get; set; } = string.Empty;

        // Foreign keys
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
