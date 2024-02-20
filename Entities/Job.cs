using Joboard.Entities;
using System.ComponentModel.DataAnnotations;

namespace Joboard.Entities
{
    public class Job : Activity
    {
        [Key]
        public int JobId { get; set; }
        [Required]
        public string JobTitle { get; set; } = string.Empty;
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
        public DateTime Deadline { get; set; } = DateTime.Now;
        [Required]
        public string Requirements { get; set; } = string.Empty;
        [Required]
        public string Skill { get; set; } = string.Empty;
        [Required]
        public bool isRemote { get; set; } = false;
        [Required]
        public int NumberOfVacancies { get; set; }
        [Required]
        public string Type { get; set; } = string.Empty;

        // Foreign keys
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int CompanyId { get; set; }
    }
}
