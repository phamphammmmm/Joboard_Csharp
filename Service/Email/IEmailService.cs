using Joboard.DTO.Email;

namespace Joboard.Service.Email
{
    public interface IEmailService
    {
        public void SendEmails(string senderEmail, string subject, string body);
        void SendEmail(string message);
        void StartConsuming();
        void Close();
    }
}
