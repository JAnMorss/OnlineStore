using OnlineStoreAPI.Shared.Kernel.Application.Command;

namespace OnlineStore.Application.Users.Commands.RegisterUser
{
    public sealed record RegisterUserCommand(
        string UserName,
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        string Password) : ICommand<Guid>;
}
