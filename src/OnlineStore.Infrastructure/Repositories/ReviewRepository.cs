using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Domain.Reviews.Entities;
using OnlineStoreAPI.Domain.Reviews.Interfaces;

namespace OnlineStore.Infrastructure.Repositories
{
    internal sealed class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<double> GetAverageRatingAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            var reviews = _context.Reviews
                .Where(r => r.ProductId == productId);

            return await reviews.AnyAsync(cancellationToken)
                ? await reviews.AverageAsync(r => r.Rating.Value, cancellationToken)
                : 0.0;
        }

        public async Task<Review?> GetByCustomerAndProductAsync(Guid customerId, Guid productId, CancellationToken cancellationToken = default)
        {
            return await _context.Reviews
               .FirstOrDefaultAsync(r => r.CustomerId == customerId && r.ProductId == productId, cancellationToken);
        }

        public async Task<IEnumerable<Review>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _context.Reviews
                .Where(r => r.ProductId == productId)
                .ToListAsync(cancellationToken);
        }
    }
}
