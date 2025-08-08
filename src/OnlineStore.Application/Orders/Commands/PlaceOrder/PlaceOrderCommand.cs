using OnlineStoreAPI.Shared.Kernel.Application.Command;
using OnlineStore.Application.Orders.DTOs;
using OnlineStoreAPI.Domain.Payments.ValueObjects;
using OnlineStoreAPI.Domain.Shared;

namespace OnlineStore.Application.Orders.Commands.PlaceOrder
{
    public sealed record PlaceOrderCommand(
        Guid UserId,
        AddressDto BillingAddress,
        AddressDto ShippingAddress,
        Guid ProductId,
        int Quantity,
        PaymentMethod PaymentMethod,
        Currency Currency) : ICommand<Guid>;
}
