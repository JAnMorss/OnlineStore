namespace OnlineStoreAPI.Shared.Kernel.Application.Clock;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
