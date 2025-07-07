using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStoreAPI.Domain.OrderItems.Entities;
using OnlineStoreAPI.Domain.Orders.Entities;
using OnlineStoreAPI.Domain.Shared;

namespace OnlineStore.Infrastructure.Configuration
{
    internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.TotalAmount, p =>
            {
                p.Property(m => m.Amount).HasColumnName("TotalAmount_Amount");
                p.Property(m => m.Currency)
                    .HasConversion(c => c.Code, v => Currency.FromCode(v))
                    .HasColumnName("TotalAmount_Currency");
            });

            builder.OwnsOne(x => x.BillingAddress);
            builder.OwnsOne(x => x.ShippingAddress);

            builder.HasMany(typeof(OrderItem)).WithOne().HasForeignKey("OrderId");
        }
    }
}
