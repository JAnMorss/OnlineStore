using OnlineStoreAPI.Domain.Categories.Entities;
using OnlineStoreAPI.Shared.Kernel.Helpers;

namespace OnlineStoreAPI.Domain.Categories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync(QueryObject query, CancellationToken cancellationToken = default);

        Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Category>> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

        Task AddAsync(Category category, CancellationToken cancellationToken = default);

        Task UpdateAsync(Category category, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<int> CountAsync(CancellationToken cancellationToken = default);

    }
}
