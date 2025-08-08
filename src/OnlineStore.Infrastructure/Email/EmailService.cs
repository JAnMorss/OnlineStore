using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using OnlineStoreAPI.Shared.Kernel.Application.Email;


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
            var smtpPort = _config["SmtpSettings:Port"];
            if (string.IsNullOrEmpty(smtpPort))
            {
                throw new InvalidOperationException("SMTP port configuration is missing.");
            }

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(
                _config["SmtpSettings:SenderName"] ?? throw new InvalidOperationException("Sender name is missing."),
                _config["SmtpSettings:SenderEmail"] ?? throw new InvalidOperationException("Sender email is missing.")));

            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(TextFormat.Html) { Text = message };

            using var client = new SmtpClient();
            await client.ConnectAsync(
                _config["SmtpSettings:Server"] ?? throw new InvalidOperationException("SMTP server configuration is missing."),
                int.Parse(smtpPort),
                MailKit.Security.SecureSocketOptions.StartTls);

            await client.AuthenticateAsync(
                _config["SmtpSettings:SenderEmail"] ?? throw new InvalidOperationException("Sender email is missing."),
                _config["SmtpSettings:Password"] ?? throw new InvalidOperationException("SMTP password is missing."));

            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }

    }
}
