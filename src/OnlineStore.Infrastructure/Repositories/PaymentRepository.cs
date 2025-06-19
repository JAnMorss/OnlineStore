using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Domain.Payments.Entities;
using OnlineStoreAPI.Domain.Payments.Interfaces;

namespace OnlineStore.Infrastructure.Repositories
{
    internal sealed class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Payment?> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Payments
               .FirstOrDefaultAsync(p => p.OrderId == orderId, cancellationToken);
        }
    }
}
