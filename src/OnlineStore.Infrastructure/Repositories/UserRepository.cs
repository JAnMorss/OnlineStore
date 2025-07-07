using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Domain.Users.Entities;
using OnlineStoreAPI.Domain.Users.Interface;

namespace OnlineStore.Infrastructure.Repositories
{
    internal sealed class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Users
               .FirstOrDefaultAsync(u => u.Email.Value == email, cancellationToken);
        }
    }
}
