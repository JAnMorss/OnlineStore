using System.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Domain.Categories.Entities;
using OnlineStoreAPI.Domain.OrderItems.Entities;
using OnlineStoreAPI.Domain.Orders.Entities;
using OnlineStoreAPI.Domain.Payments.Entities;
using OnlineStoreAPI.Domain.Products.Entities;
using OnlineStoreAPI.Domain.Reviews.Entities;
using OnlineStoreAPI.Domain.Users.Entities;
using OnlineStoreAPI.Shared.Kernel;
using OnlineStoreAPI.Shared.Kernel.Domain;

namespace OnlineStore.Infrastructure
{
    public sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly IPublisher _publisher;

        public ApplicationDbContext(DbContextOptions options, IPublisher publisher)
            : base(options)
        {
            _publisher = publisher;
        }

        // Products
        public DbSet<Product> Products { get; set; } 
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Review> Reviews { get; set; } 

        // Orders
        public DbSet<Order> Orders { get; set; } 
        public DbSet<OrderItem> OrderItems { get; set; } 
        public DbSet<Payment> Payments { get; set; } 

        // Users
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                int result = await base.SaveChangesAsync(cancellationToken);
                await PublishDomainEventsAsync();
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ArgumentException("A concurrency exception occurred during saving.", ex);
            }
        }

        private async Task PublishDomainEventsAsync()
        {
            var domainEvents = ChangeTracker
                .Entries<Entity>()
                .Select(e => e.Entity)
                .SelectMany(e =>
                {
                    var events = e.GetDomainEvents();
                    e.ClearDomainEvents();
                    return events;
                })
                .ToList();

            if (domainEvents.Count == 0)
                return;

            var tasks = domainEvents.Select(@event => _publisher.Publish(@event));
            await Task.WhenAll(tasks);
        }
    }
}
