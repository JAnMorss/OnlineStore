using OnlineStore.Application.Orders.DTOs;
using OnlineStoreAPI.Shared.Kernel.Application.Query;

namespace OnlineStore.Application.Orders.Queries.GetOrderById
{
    public sealed record GetOrderByIdQuery(Guid OrderId) : IQuery<OrderDto>;
}
