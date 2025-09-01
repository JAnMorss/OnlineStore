using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStoreAPI.Domain.Payments.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;
using OnlineStore.Application.Payments.Responses;

namespace OnlineStore.Application.Payments.Queries.GetPaymentsByOrder
{
    public sealed class GetPaymentsByOrderQueryHandler
        : IQueryHandler<GetPaymentsByOrderQuery, List<PaymentResponse>>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentsByOrderQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Result<List<PaymentResponse>>> Handle(GetPaymentsByOrderQuery request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByOrderIdAsync(request.OrderId, cancellationToken);

            var result = payment
                .OrderByDescending(p => p.PaymentDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(PaymentResponse.FromEntity)
                .ToList();

            return Result.Success(result);
        }
    }
}
