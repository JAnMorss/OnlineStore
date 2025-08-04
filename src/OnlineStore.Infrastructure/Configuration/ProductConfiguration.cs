using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStoreAPI.Domain.Products.Entities;
using OnlineStoreAPI.Domain.Products.ValueObjects;
using OnlineStoreAPI.Domain.Shared;

namespace OnlineStore.Infrastructure.Configuration
{
    internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasConversion(name => name.Value, value => new ProductName(value));

            builder.Property(p => p.Description)
                .HasConversion(desc => desc.Value, value => new ProductDescription(value));

            builder.Property(s => s.Stock)
                .HasConversion(sto => sto.Quantity, value => new Stock(value));

            builder.OwnsOne(p => p.Price, p =>
            {
                p.Property(p => p.Amount)
                    .HasColumnName("Price_Amount")
                    .HasPrecision(18, 4);

                p.Property(p => p.Currency)
                    .HasConversion(c => c.Code, value => Currency.FromCode(value))
                    .HasColumnName("Price_Currency");
            });

        }
    }
}
