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
