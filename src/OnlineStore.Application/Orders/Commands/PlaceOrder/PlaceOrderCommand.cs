using OnlineStoreAPI.Shared.Kernel.Application.Command;
using OnlineStoreAPI.Domain.Payments.ValueObjects;
using OnlineStoreAPI.Domain.Shared;
using OnlineStore.Application.Orders.Responses;

namespace OnlineStore.Application.Orders.Commands.PlaceOrder
{
    public sealed record PlaceOrderCommand(
        Guid UserId,
        AddressResponse BillingAddress,
        AddressResponse ShippingAddress,
        Guid ProductId,
        int Quantity,
        PaymentMethod PaymentMethod,
        Currency Currency) : ICommand<Guid>;
}
