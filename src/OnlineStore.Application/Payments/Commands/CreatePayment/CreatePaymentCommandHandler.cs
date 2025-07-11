using OnlineStore.Application.Abstractions.Messaging;
using OnlineStoreAPI.Domain.Orders.Errors;
using OnlineStoreAPI.Domain.Orders.Interfaces;
using OnlineStoreAPI.Domain.Payments.Entities;
using OnlineStoreAPI.Domain.Payments.Interfaces;
using OnlineStoreAPI.Domain.Payments.ValueObjects;
using OnlineStoreAPI.Domain.Shared;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Payments.Commands.CreatePayment
{
    public sealed class CreatePaymentCommandHandler : ICommandHandler<CreatePaymentCommand, Guid>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePaymentCommandHandler(
            IPaymentRepository paymentRepository, 
            IOrderRepository orderRepository, 
            IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order is null)
                return Result.Failure<Guid>(OrderErrors.NotFound);

            var currency = Currency.FromCode(request.CurrencyCode);
            if (currency == Currency.None)
                return Result.Failure<Guid>(new Error("Currency.Invalid", $"Unsupported currency code: {request.CurrencyCode}"));


            var amount = new Money(request.Amount, currency);

            var payment = new Payment(
                Guid.NewGuid(),
                request.OrderId,
                amount,
                request.PaymentDate,
                request.PaymentMethod,
                PaymentStatus.Pending
            );

            await _paymentRepository.AddAsync(payment, cancellationToken);

            order.AttachPayment(payment);

            await _orderRepository.UpdateAsync(order, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(payment.Id);
        }
    }
}
