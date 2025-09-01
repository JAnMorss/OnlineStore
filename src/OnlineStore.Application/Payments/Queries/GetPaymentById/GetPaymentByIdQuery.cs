using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStore.Application.Payments.Responses;

namespace OnlineStore.Application.Payments.Queries.GetPaymentById
{
    public sealed record GetPaymentByIdQuery(Guid PaymentId) : IQuery<PaymentResponse>;
}
