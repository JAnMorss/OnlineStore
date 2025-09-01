using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStoreAPI.Domain.Orders.Errors;
using OnlineStoreAPI.Domain.Orders.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;
using OnlineStore.Application.Orders.Responses;

namespace OnlineStore.Application.Orders.Queries.GetOrdersByUser
{
    public sealed class GetOrdersByUserQueryHandler
        : IQueryHandler<GetOrdersByUserQuery, List<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersByUserQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<List<OrderResponse>>> Handle(GetOrdersByUserQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            if (orders is null || !orders.Any())
                return Result.Failure<List<OrderResponse>>(OrderErrors.NotFound);

            var result = orders
                .Select(OrderResponse.FromEntity)
                .ToList();

            return Result.Success(result);
        }
    }
}
