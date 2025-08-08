using OnlineStoreAPI.Shared.Kernel.Application.Command;
using OnlineStoreAPI.Domain.Orders.Errors;
using OnlineStoreAPI.Domain.Orders.Interfaces;
using OnlineStoreAPI.Domain.Payments.Errors;
using OnlineStoreAPI.Domain.Payments.Interfaces;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Orders.Commands.AttachPaymentToOrder
{
    public sealed class AttachPaymentToOrderCommandHandler
        : ICommandHandler<AttachPaymentToOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentRepository _paymentRepository;

        public AttachPaymentToOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IPaymentRepository paymentRepository)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _paymentRepository = paymentRepository;
        }

        public async Task<Result<Guid>> Handle(AttachPaymentToOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order is null)
                return Result.Failure<Guid>(OrderErrors.NotFound);

            var payment = await _paymentRepository.GetByIdAsync(request.PaymentId);
            if (payment is null)
                return Result.Failure<Guid>(PaymentErrors.NotFound);

            order.AttachPayment(payment);

            await _orderRepository.UpdateAsync(order, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(order.Id);
        }
    }
}
