using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Domain.Products.Entities;
using OnlineStoreAPI.Domain.Products.Interfaces;

namespace OnlineStore.Infrastructure.Repositories
{
    internal sealed class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync(cancellationToken);
        }

        public async Task<(IEnumerable<Product> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, string? sortBy, bool descending, CancellationToken cancellationToken = default)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _context.Products.AsQueryable();

            query = sortBy?.ToLower() switch
            {
                "price" => descending
                    ? query.OrderByDescending(p => p.Price.Amount)
                    : query.OrderBy(p => p.Price.Amount),
                "name" => descending
                    ? query.OrderByDescending(p => p.Name.Value)
                    : query.OrderBy(p => p.Name.Value),
                _=>query
            };

            var totalCount = await query.CountAsync(cancellationToken);
            var pagedItems = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (pagedItems, totalCount);
        }

        public async Task<IEnumerable<Product>> SearchAsync(string keyword, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            return await _context.Products
                .Where(p => p.Name.Value.Contains(keyword))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
