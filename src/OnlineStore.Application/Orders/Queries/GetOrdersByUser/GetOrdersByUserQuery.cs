using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Orders.DTOs;

namespace OnlineStore.Application.Orders.Queries.GetOrdersByUser
{
    public sealed record GetOrdersByUserQuery(Guid UserId) : IQuery<List<OrderDto>>;
}
