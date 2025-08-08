namespace OnlineStoreAPI.Shared.Kernel.Application.Email;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string message);
}
