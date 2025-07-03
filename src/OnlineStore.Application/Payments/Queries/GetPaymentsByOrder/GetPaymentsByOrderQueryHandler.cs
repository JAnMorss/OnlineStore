using OnlineStore.Application.Abstractions.Messaging;
using OnlineStore.Application.Payments.DTOs;
using OnlineStoreAPI.Domain.Payments.Interfaces;
using OnlineStoreAPI.Shared.Kernel.ErrorHandling;

namespace OnlineStore.Application.Payments.Queries.GetPaymentsByOrder
{
    public sealed class GetPaymentsByOrderQueryHandler
        : IQueryHandler<GetPaymentsByOrderQuery, List<PaymentDto>>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentsByOrderQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Result<List<PaymentDto>>> Handle(GetPaymentsByOrderQuery request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByOrderIdAsync(request.OrderId, cancellationToken);

            var result = payment
                .OrderByDescending(p => p.PaymentDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(PaymentDto.FromEntity)
                .ToList();

            return Result.Success(result);
        }
    }
}
