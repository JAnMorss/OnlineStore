using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Domain.Categories.Entities;
using OnlineStoreAPI.Domain.Categories.Interfaces;
using OnlineStoreAPI.Domain.Products.Entities;
using OnlineStoreAPI.Shared.Kernel.Helpers;

namespace OnlineStore.Infrastructure.Repositories
{
    internal sealed class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<Category>().CountAsync(cancellationToken);
        }

        public override async Task<IEnumerable<Category>> GetAllAsync(QueryObject query, CancellationToken cancellationToken = default)
        {
            var categories = _context.Categories.Include(p => p.Products).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                categories = query.SortBy?.ToLower() switch
                {
                    "name" => query.Descending ? categories.OrderByDescending(p => p.Name) : categories.OrderBy(p => p.Name),
                    _ => categories
                };
            }

            var skip = (query.Page - 1) * query.PageSize;

            return await categories.Skip(skip).Take(query.PageSize).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Category>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .Where(c => c.Name.Value.Contains(name))
                .ToListAsync(cancellationToken);
        }


    }
}
