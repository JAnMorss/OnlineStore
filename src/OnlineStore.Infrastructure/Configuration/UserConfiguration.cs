using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Domain.Users.Entities;
using OnlineStoreAPI.Domain.Users.ValueObjects;

namespace OnlineStore.Infrastructure.Configuration
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserName)
                .HasConversion(x => x.Value, v => new UserName(v))
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .HasConversion(x => x.Value, v => new EmailVO(v))
                .HasMaxLength(200);

            builder.Property(x => x.Role)
                .HasConversion(x => x.Value, v => new Role(v));

            builder.OwnsOne(x => x.Address);

            builder.OwnsOne(x => x.CustomerProfile, cp =>
            {
                cp.Property(p => p.FirstName)
                    .HasConversion(x => x.Value, v => new FirstName(v));

                cp.Property(p => p.LastName)
                    .HasConversion(x => x.Value, v => new LastName(v));

                cp.Property(p => p.PhoneNumber)
                    .HasConversion(x => x.Value, v => new PhoneNumber(v));

                cp.OwnsOne(p => p.Address, a =>
                {
                    a.Property(x => x.Street).HasColumnName("Customer_Street");
                    a.Property(x => x.City).HasColumnName("Customer_City");
                    a.Property(x => x.Barangay).HasColumnName("Customer_Barangay");
                    a.Property(x => x.ZipCode).HasColumnName("Customer_ZipCode");
                    a.Property(x => x.Country).HasColumnName("Customer_Country");
                });
            });


            builder.OwnsOne(x => x.SellerProfile, sp =>
            {
                sp.Property(p => p.ShopName)
                    .HasConversion(x => x.Value, v => new ShopName(v));
                sp.Property(p => p.StoreDescription)
                    .HasConversion(x => x.Value, v => new StoreDescription(v));
                sp.Property(p => p.PhoneNumber)
                    .HasConversion(x => x.Value, v => new PhoneNumber(v));
                sp.OwnsOne(p => p.Address, a =>
                {
                    a.Property(x => x.Street).HasColumnName("Customer_Street");
                    a.Property(x => x.City).HasColumnName("Customer_City");
                    a.Property(x => x.Barangay).HasColumnName("Customer_Barangay");
                    a.Property(x => x.ZipCode).HasColumnName("Customer_ZipCode");
                    a.Property(x => x.Country).HasColumnName("Customer_Country");
                });
            });
        }
    }

}


