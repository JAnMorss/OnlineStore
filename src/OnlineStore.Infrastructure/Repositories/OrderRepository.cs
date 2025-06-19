using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Domain.Orders.Entities;
using OnlineStoreAPI.Domain.Orders.Interfaces;

namespace OnlineStore.Infrastructure.Repositories
{
    internal sealed class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(o => o.OrderItems)
                .Include(o => o.Payment)
                .Where(o => o.UserId == userId)
                .ToListAsync(cancellationToken);

        }
    }
}
