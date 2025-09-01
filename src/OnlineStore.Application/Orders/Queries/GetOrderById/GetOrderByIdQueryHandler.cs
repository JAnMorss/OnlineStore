using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStoreAPI.Domain.Orders.Errors;
using OnlineStoreAPI.Domain.Orders.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;
using OnlineStore.Application.Orders.Responses;

namespace OnlineStore.Application.Orders.Queries.GetOrderById
{
    public sealed class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderResponse>
    {
        private readonly IOrderRepository _repository;

        public GetOrderByIdQueryHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order is null)
                return Result.Failure<OrderResponse>(OrderErrors.NotFound);

            return Result.Success(OrderResponse.FromEntity(order));
        }
    }
}
