using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStoreAPI.Domain.Payments.Entities;
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
                p.Property(m => m.Amount).HasColumnName("Amount");
                p.Property(m => m.Currency)
                    .HasConversion(c => c.Code, v => Currency.FromCode(v))
                    .HasColumnName("Currency");
            });
        }
    }
}
