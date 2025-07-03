using OnlineStoreAPI.Domain.Payments.Entities;

namespace OnlineStoreAPI.Domain.Payments.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<Payment>> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default);
        Task AddAsync(Payment payment, CancellationToken cancellationToken = default);
    }
}
