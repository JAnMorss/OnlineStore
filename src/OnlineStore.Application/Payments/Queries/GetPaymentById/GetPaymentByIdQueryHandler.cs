using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStore.Application.Payments.DTOs;
using OnlineStoreAPI.Domain.Payments.Errors;
using OnlineStoreAPI.Domain.Payments.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Payments.Queries.GetPaymentById
{
    public sealed class GetPaymentByIdQueryHandler
        : IQueryHandler<GetPaymentByIdQuery, PaymentDto>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentByIdQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Result<PaymentDto>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.PaymentId, cancellationToken);
            if (payment == null) 
                return Result.Failure<PaymentDto>(PaymentErrors.NotFound);

            var result = PaymentDto.FromEntity(payment);

            return Result.Success(result);

        }
    }
}
