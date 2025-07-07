using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using OnlineStore.Application.Abstractions.Email;

namespace OnlineStore.Infrastructure.Email
{
    internal sealed class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }


        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(
                _config["SmtpSettings:SenderName"],
                _config["SmtpSettings:SenderEmail"]));

            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(TextFormat.Html) { Text = message };

            using var client = new SmtpClient();
            await client.ConnectAsync(
                _config["SmtpSettings:Server"],
                int.Parse(_config["SmtpSettings:Port"]),
                MailKit.Security.SecureSocketOptions.StartTls);

            await client.AuthenticateAsync(
                _config["SmtpSettings:SenderEmail"],
                _config["SmtpSettings:Password"]);

            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
