using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStoreAPI.Domain.Payments.Errors;
using OnlineStoreAPI.Domain.Payments.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;
using OnlineStore.Application.Payments.Responses;

namespace OnlineStore.Application.Payments.Queries.GetPaymentById
{
    public sealed class GetPaymentByIdQueryHandler
        : IQueryHandler<GetPaymentByIdQuery, PaymentResponse>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentByIdQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Result<PaymentResponse>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.PaymentId, cancellationToken);
            if (payment == null) 
                return Result.Failure<PaymentResponse>(PaymentErrors.NotFound);

            var result = PaymentResponse.FromEntity(payment);

            return Result.Success(result);

        }
    }
}
