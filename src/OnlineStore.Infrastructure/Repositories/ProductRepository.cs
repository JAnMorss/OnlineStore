using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Domain.Products.Entities;
using OnlineStoreAPI.Domain.Products.Interfaces;
using OnlineStoreAPI.Shared.Kernel.Helpers;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

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

        private (List<Product> Items, int TotalCount) ApplyPagingAndSorting(
            List<Product> products,
            QueryObject queryObject)
        {
            IEnumerable<Product> sorted = products;

            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                switch (queryObject.SortBy.ToLower())
                {
                    case "price":
                        sorted = queryObject.Descending
                            ? sorted.OrderByDescending(p => p.Price.Amount)
                            : sorted.OrderBy(p => p.Price.Amount);
                        break;

                    case "name":
                        sorted = queryObject.Descending
                            ? sorted.OrderByDescending(p => p.Name.Value)
                            : sorted.OrderBy(p => p.Name.Value);
                        break;

                    default:
                        sorted = sorted.OrderBy(p => p.Name.Value);
                        break;
                }
            }
            else
            {
                sorted = sorted.OrderBy(p => p.Name.Value);
            }

            int totalCount = sorted.Count();

            var pagedItems = sorted
                .Skip((queryObject.Page - 1) * queryObject.PageSize)
                .Take(queryObject.PageSize)
                .ToList();

            return (pagedItems, totalCount);
        }


        public async Task<(IEnumerable<Product> Items, int TotalCount)> GetPagedAsync(
            QueryObject query,
            CancellationToken cancellationToken = default)
        {
            var products = await _context.Products.ToListAsync(cancellationToken);

            return ApplyPagingAndSorting(products, query);
        }


        public async Task<(IEnumerable<Product> Items, int TotalCount)> SearchAsync(
            QueryObject query,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(query.Keyword))
            {
                return (Enumerable.Empty<Product>(), 0);
            }

            var products = await _context.Products.ToListAsync(cancellationToken);

            var filtered = products
                .Where(p => p.Name.Value.Contains(query.Keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return ApplyPagingAndSorting(filtered, query);
        }
    }
}
