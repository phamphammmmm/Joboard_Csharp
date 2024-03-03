using Microsoft.AspNetCore.Mvc;
using Joboard.Service.Email;

namespace Joboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public IActionResult SendEmail([FromBody] EmailRequestModel model)
        {
            // Gửi email
            _emailService.SendEmails(model.SenderEmail, model.Subject, model.Body);
            _emailService.StartConsuming();
            return Ok("Email sent successfully");
        }
    }

    public class EmailRequestModel
    {
        public string SenderEmail { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;
    }
}
