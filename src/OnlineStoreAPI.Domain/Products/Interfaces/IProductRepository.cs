using OnlineStoreAPI.Domain.Products.Entities;
using OnlineStoreAPI.Shared.Kernel.Helpers;

namespace OnlineStoreAPI.Domain.Products.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<(IEnumerable<Product> Items, int TotalCount)> SearchAsync(QueryObject query, CancellationToken cancellationToken = default);

        Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default);

        Task<(IEnumerable<Product> Items, int TotalCount)> GetPagedAsync(QueryObject query, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);


        Task AddAsync(Product product, CancellationToken cancellationToken = default);

        Task UpdateAsync(Product product, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
