using System.ComponentModel.DataAnnotations;

namespace Joboard.DTO.Email
{
    public class Email_Notification_DTO
    {
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
