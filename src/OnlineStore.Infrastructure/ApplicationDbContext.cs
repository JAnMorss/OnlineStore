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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher)
            : base(options)
        {
            _publisher = publisher;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            await PublishDomainEventsAsync();
            return result;
        }

        private async Task PublishDomainEventsAsync()
        {
            var domainEvents = ChangeTracker
                .Entries<BaseEntity>()
                .Select(e => e.Entity)
                .SelectMany(entity =>
                {
                    var events = entity.GetDomainEvents();
                    entity.ClearDomainEvents();
                    return events;
                })
                .ToList();

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }
        }
    }
}


