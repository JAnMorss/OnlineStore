using OnlineStoreAPI.Domain.Orders.Entities;

namespace OnlineStoreAPI.Domain.Orders.Interfaces
{
    public interface IOrderRepository
    {

        Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default);

        Task<IEnumerable<Order>> GetByUserIdAsync(Guid customerId, CancellationToken cancellationToken = default);

        Task AddAsync(Order order, CancellationToken cancellationToken = default);

        Task UpdateAsync(Order order, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid orderId, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(Guid orderId, CancellationToken cancellationToken = default);
    }
}
