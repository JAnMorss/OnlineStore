using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
                .HasConversion(
                    userName => userName.Value,
                    value => new UserName(value))
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.FirstName)
                .HasConversion(
                    firstName => firstName.Value,
                    value => new FirstName(value))
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasConversion(
                    lastName => lastName.Value,
                    value => new LastName(value))
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasConversion(
                    email => email.Value,
                    value => new EmailVO(value))
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasConversion(
                    phoneNumber => phoneNumber.Value,
                    value => new PhoneNumber(value))
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.IdentityId)
                .HasMaxLength(200);

            builder.HasIndex(user => user.Email).IsUnique();

            builder.HasIndex(user => user.IdentityId).IsUnique();
        }
    }
}
