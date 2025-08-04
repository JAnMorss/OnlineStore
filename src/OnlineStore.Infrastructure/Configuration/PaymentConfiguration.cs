using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStoreAPI.Domain.Payments.Entities;
using OnlineStoreAPI.Domain.Payments.ValueObjects;
using OnlineStoreAPI.Domain.Shared;

namespace OnlineStore.Infrastructure.Configuration
{
    internal sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.Id);

            builder.OwnsOne(x => x.Amount, p =>
            {
                p.Property(m => m.Amount)
                    .HasColumnName("Amount")
                    .HasPrecision(18, 4); 

                p.Property(m => m.Currency)
                    .HasConversion(c => c.Code, v => Currency.FromCode(v))
                    .HasColumnName("Currency");
            });


            builder.Property(x => x.PaymentMethod)
                .HasConversion(
                    pm => pm.Value,
                    value => PaymentMethod.FromValue(value)
                )
                .HasColumnName("PaymentMethod")
                .HasMaxLength(100);

            builder.Property(x => x.PaymentStatus)
                .HasConversion(
                    status => status.Value,
                    value => PaymentStatus.FromValue(value)
                )
                .HasColumnName("PaymentStatus")
                .HasMaxLength(50);

            builder.HasOne(p => p.Order)
               .WithOne(o => o.Payment)
               .HasForeignKey<Payment>(p => p.OrderId);

            builder.HasIndex(p => p.OrderId).IsUnique();
        }
    }
}
