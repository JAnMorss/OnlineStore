using OnlineStoreAPI.Shared.Kernel.Application.Command;
using OnlineStoreAPI.Domain.Orders.Errors;
using OnlineStoreAPI.Domain.Orders.Interfaces;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Orders.Commands.MarkOrderAsShipped
{
    public sealed class MarkOrderAsShippedCommandHandler : ICommandHandler<MarkOrderAsShippedCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MarkOrderAsShippedCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(MarkOrderAsShippedCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order is null)
                return Result.Failure(OrderErrors.NotFound);

            var shipResult = order.MarkAsShipped();
            if(shipResult.IsFailure)
                return Result.Failure(shipResult.Error);

            await _orderRepository.UpdateAsync(order, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
