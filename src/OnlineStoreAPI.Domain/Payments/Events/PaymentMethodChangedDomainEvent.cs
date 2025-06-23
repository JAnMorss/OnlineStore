using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStoreAPI.Domain.Payments.Events
{
    public sealed record PaymentMethodChangedDomainEvent(Guid PaymentId) : IDomainEvent;
}
