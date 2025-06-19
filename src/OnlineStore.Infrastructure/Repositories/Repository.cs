using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStore.Infrastructure.Repositories
{
    internal abstract class Repository<T> where T : Entity
    {
        protected readonly ApplicationDbContext _context;

        protected Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context
                .Set<T>()
                .ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context
                .Set<T>()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context
                .Set<T>()
                .AddAsync(entity, cancellationToken);
        }

        public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context
                .Set<T>()
                .Update(entity);
            return Task.CompletedTask;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                return true;
            }

            return false;
        }


        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context
                .Set<T>()
                .AnyAsync(x => x.Id == id, cancellationToken);
        }

    }
}
