using OnlineStore.Application.Orders.Responses;
using OnlineStoreAPI.Shared.Kernel.Application.Query;

namespace OnlineStore.Application.Orders.Queries.GetOrderById
{
    public sealed record GetOrderByIdQuery(Guid OrderId) : IQuery<OrderResponse>;
}
