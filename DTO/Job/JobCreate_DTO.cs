using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Joboard.DTO.Job
{
    public class JobCreate_DTO
    {
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
        public DateTime Create_at { get; set; }

        [Required]
        public int UserId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int CompanyId { get; set; }
    }
}
