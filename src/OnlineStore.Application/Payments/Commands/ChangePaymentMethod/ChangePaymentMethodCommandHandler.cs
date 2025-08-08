using OnlineStoreAPI.Shared.Kernel.Application.Command;
using OnlineStoreAPI.Domain.Payments.Errors;
using OnlineStoreAPI.Domain.Payments.Interfaces;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Payments.Commands.ChangePaymentMethod
{
    public sealed class ChangePaymentMethodCommandHandler
        : ICommandHandler<ChangePaymentMethodCommand, Guid>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangePaymentMethodCommandHandler(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(ChangePaymentMethodCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.PaymentId, cancellationToken);
            if (payment is null)
                return Result.Failure<Guid>(PaymentErrors.NotFound);

            payment.ChangePaymentMethod(request.NewMethod);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(payment.Id);
        }
    }
}
