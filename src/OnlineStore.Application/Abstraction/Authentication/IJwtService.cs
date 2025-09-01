using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Abstraction.Authentication;

public interface IJwtService
{
    Task<Result<string>> GetAccessTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);
}