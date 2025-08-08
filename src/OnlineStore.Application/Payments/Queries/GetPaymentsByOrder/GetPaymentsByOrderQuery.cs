using OnlineStoreAPI.Shared.Kernel.Application.Query;
using OnlineStore.Application.Payments.DTOs;

namespace OnlineStore.Application.Payments.Queries.GetPaymentsByOrder
{
    public sealed record GetPaymentsByOrderQuery(
        Guid OrderId,
        int PageNumber = 1,
        int PageSize = 10) : IQuery<List<PaymentDto>>;
}
