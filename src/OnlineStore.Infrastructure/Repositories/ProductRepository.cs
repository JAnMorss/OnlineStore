using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Domain.Products.Entities;
using OnlineStoreAPI.Domain.Products.Interfaces;
using OnlineStoreAPI.Shared.Kernel.Helpers;

namespace OnlineStore.Infrastructure.Repositories
{
    internal sealed class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<Product>().CountAsync(cancellationToken);
        }

        public override async Task<IEnumerable<Product>> GetAllAsync(QueryObject query, CancellationToken cancellationToken = default)
        {
            var products = _context.Products.Include(p => p.Category).AsQueryable();

            products = query.SortBy?.ToLower() switch
            {
                "name" => query.Descending ? products.OrderByDescending(p => p.Name) : products.OrderBy(p => p.Name),
                _ => products
            };

            var skip = (query.Page - 1) * query.PageSize;

            return await products.Skip(skip).Take(query.PageSize).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync(cancellationToken);
        }

        public async Task<(IEnumerable<Product> Items, int TotalCount)> SearchAsync(
           SearchQueryObject query,
           CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(query.Keyword))
            {
                return (Enumerable.Empty<Product>(), 0);
            }

            var filteredQuery = _context.Products
                .Where(p => p.Name.Value.Contains(query.Keyword));

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                filteredQuery = query.Descending
                    ? filteredQuery.OrderByDescending(p => EF.Property<object>(p, query.SortBy))
                    : filteredQuery.OrderBy(p => EF.Property<object>(p, query.SortBy));
            }

            var totalCount = await filteredQuery.CountAsync(cancellationToken);

            var pagedItems = await filteredQuery
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(cancellationToken);

            return (pagedItems, totalCount);
        }

    }
}
