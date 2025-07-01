using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Orders.DTOs;

namespace OnlineStore.Application.Orders.Queries.GetOrderById
{
    public sealed record GetOrderByIdQuery(Guid OrderId) : IQuery<OrderDto>;
}
