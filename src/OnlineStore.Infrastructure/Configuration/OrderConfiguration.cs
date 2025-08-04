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
                p.Property(m => m.Amount)
                    .HasColumnName("TotalAmount_Amount")
                    .HasPrecision(18, 2);

                p.Property(m => m.Currency)
                    .HasConversion(c => c.Code, v => Currency.FromCode(v))
                    .HasColumnName("TotalAmount_Currency");
            });

            builder.OwnsOne(x => x.BillingAddress, b =>
            {
                b.Property(p => p.Street).HasColumnName("BillingAddress_Street");
                b.Property(p => p.City).HasColumnName("BillingAddress_City");
                b.Property(p => p.Barangay).HasColumnName("BillingAddress_Barangay");
                b.Property(p => p.ZipCode).HasColumnName("BillingAddress_ZipCode");
                b.Property(p => p.Country).HasColumnName("BillingAddress_Country");
            });

            builder.OwnsOne(x => x.ShippingAddress, s =>
            {
                s.Property(p => p.Street).HasColumnName("ShippingAddress_Street");
                s.Property(p => p.City).HasColumnName("ShippingAddress_City");
                s.Property(p => p.Barangay).HasColumnName("ShippingAddress_Barangay");
                s.Property(p => p.ZipCode).HasColumnName("ShippingAddress_ZipCode");
                s.Property(p => p.Country).HasColumnName("ShippingAddress_Country");
            });

            builder.HasMany(o => o.OrderItems)
                   .WithOne(oi => oi.Order)
                   .HasForeignKey(oi => oi.OrderId);
        }
    }
}
