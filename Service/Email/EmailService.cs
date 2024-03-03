using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Joboard.Service.User;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Joboard.Service.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string ExchangeName = "email_exchange";
        private const string QueueName = "email_queue";
        private const string RoutingKey = "email";

        public EmailService(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "admin",
                Password = "12345678",
                VirtualHost = "/"
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                // Declare exchange type
                _channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Direct);

                // Declare queue
                _channel.QueueDeclare(queue: QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                // Bind queue to exchange
                _channel.QueueBind(queue: QueueName, exchange: ExchangeName, routingKey: RoutingKey);

                Console.WriteLine("Setup is completed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void SendEmails(string senderEmail, string subject, string body)
        {
            List<string> recipientEmails = _userService.GetAllEmails();

            foreach (var recipientEmail in recipientEmails)
            {
                // Format email message
                string emailContent = $"{{\"to\": \"{recipientEmail}\", \"subject\": \"{subject}\", \"body\": \"{body}\"}}";
                var bodyBytes = Encoding.UTF8.GetBytes(emailContent);

                // Send message to queue
                _channel.BasicPublish(exchange: "", routingKey: "email_queue", basicProperties: null, body: bodyBytes);

                Console.WriteLine($"Sent email to {recipientEmail}");
            }
        }

        public void StartConsuming()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Process the message (send email)
                SendEmail(message);

                Console.WriteLine("Email sent");
            };

            _channel.BasicConsume(queue: "email_queue",
                                  autoAck: true,
                                  consumer: consumer);
        }

        public void SendEmail(string message)
        {
            dynamic emailDetails = Newtonsoft.Json.JsonConvert.DeserializeObject(message);

            string toEmail = emailDetails.to;

            // Create the email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = emailDetails.subject;
            email.Body = new TextPart(TextFormat.Html) { Text = emailDetails.body };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
                smtp.Send(email);
                smtp.Disconnect(true);
            }

            email.Dispose();
        }

        public void Close()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
