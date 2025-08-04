using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Domain.OrderItems.Entities;
using OnlineStoreAPI.Domain.OrderItems.ValueObjects;
using OnlineStoreAPI.Domain.Shared;

namespace OnlineStore.Infrastructure.Configuration
{
    internal sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantity)
                .HasConversion(x => x.Value, v => new Quantity(v));

            builder.OwnsOne(x => x.UnitPrice, p =>
            {
                p.Property(m => m.Amount)
                 .HasPrecision(18, 2) 
                 .HasColumnName("UnitPrice_Amount");

                p.Property(m => m.Currency)
                 .HasConversion(c => c.Code, v => Currency.FromCode(v))
                 .HasColumnName("UnitPrice_Currency");
            });

            builder.HasOne(o => o.Order)
               .WithMany(order => order.OrderItems) 
               .HasForeignKey(o => o.OrderId);

        }
    }
}
