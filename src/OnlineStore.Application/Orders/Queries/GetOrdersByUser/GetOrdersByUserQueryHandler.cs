using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStore.Application.Orders.DTOs;
using OnlineStoreAPI.Domain.Orders.Errors;
using OnlineStoreAPI.Domain.Orders.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Orders.Queries.GetOrdersByUser
{
    public sealed class GetOrdersByUserQueryHandler
        : IQueryHandler<GetOrdersByUserQuery, List<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersByUserQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<List<OrderDto>>> Handle(GetOrdersByUserQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            if (orders is null || !orders.Any())
                return Result.Failure<List<OrderDto>>(OrderErrors.NotFound);

            var result = orders
                .Select(OrderDto.FromEntity)
                .ToList();

            return Result.Success(result);
        }
    }
}
