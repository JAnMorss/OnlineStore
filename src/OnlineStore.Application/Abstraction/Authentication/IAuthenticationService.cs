using OnlineStoreAPI.Domain.Users.Entities;

namespace OnlineStore.Application.Abstraction.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(User user, string password, CancellationToken cancellationToken = default);
}
