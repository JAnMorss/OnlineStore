using OnlineStore.Application.Abstractions.Clock;

namespace OnlineStore.Infrastructure.Clock
{
    internal sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
