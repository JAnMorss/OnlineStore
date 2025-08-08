using OnlineStoreAPI.Shared.Kernel.Application.Command;
using OnlineStoreAPI.Domain.Payments.Errors;
using OnlineStoreAPI.Domain.Payments.Interfaces;
using OnlineStoreAPI.Domain.Shared;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Payments.Commands.ChangePaymentAmount
{
    public sealed class ChangePaymentAmountCommandHandler 
        : ICommandHandler<ChangePaymentAmountCommand, Guid>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangePaymentAmountCommandHandler(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(ChangePaymentAmountCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.PaymentId, cancellationToken);
            if (payment == null) 
                return Result.Failure<Guid>(PaymentErrors.NotFound);

            var newAmount = new Money(request.NewAmount, payment.Amount.Currency);
            payment.ChangeAmount(newAmount);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(payment.Id);
        }
    }
}
