using OnlineStore.Application.Abstractions.Messaging;
using OnlineStoreAPI.Domain.Payments.Errors;
using OnlineStoreAPI.Domain.Payments.Interfaces;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Payments.Commands.ChangePaymentStatus
{
    public sealed class ChangePaymentStatusCommandHandler 
        : ICommandHandler<ChangePaymentStatusCommand, Guid>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangePaymentStatusCommandHandler(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(ChangePaymentStatusCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.PaymentId, cancellationToken);
            if (payment is null)
                return Result.Failure<Guid>(PaymentErrors.NotFound);

            payment.ChangePaymentStatus(request.NewStatus);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(payment.Id);
        }
    }
}
