using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Orders.DTOs;
using OnlineStoreAPI.Domain.Payments.ValueObjects;
using OnlineStoreAPI.Domain.Shared;

namespace OnlineStore.Application.Orders.Commands.PlaceOrder
{
    public record PlaceOrderCommand(
        Guid UserId,
        AddressDto BillingAddress,
        AddressDto ShippingAddress,
        Guid ProductId,
        int Quantity,
        PaymentMethod PaymentMethod,
        Currency Currency) : ICommand<Guid>;
}
