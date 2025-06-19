using OnlineStoreAPI.Domain.Reviews.Entities;

namespace OnlineStoreAPI.Domain.Reviews.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

        Task<Review?> GetByCustomerAndProductAsync(Guid customerId, Guid productId, CancellationToken cancellationToken = default);

        Task AddAsync(Review review, CancellationToken cancellationToken = default);

        Task UpdateAsync(Review review, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid reviewId, CancellationToken cancellationToken = default);

        Task<double> GetAverageRatingAsync(Guid productId, CancellationToken cancellationToken = default);
    }
}
