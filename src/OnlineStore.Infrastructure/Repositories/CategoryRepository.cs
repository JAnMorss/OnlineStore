using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Domain.Categories.Entities;
using OnlineStoreAPI.Domain.Categories.Interfaces;

namespace OnlineStore.Infrastructure.Repositories
{
    internal sealed class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .Where(c => c.Name.Value.Contains(name))
                .ToListAsync(cancellationToken);
        }

        public async Task<(IEnumerable<Category> Categories, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Categories.AsQueryable();

            var totalCount = await query.CountAsync(cancellationToken);

            var categories = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (categories, totalCount);
        }

    }
}
