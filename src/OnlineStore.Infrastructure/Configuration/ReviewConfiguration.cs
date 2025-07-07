using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Domain.Reviews.Entities;
using OnlineStoreAPI.Domain.Reviews.ValueObjects;

namespace OnlineStore.Infrastructure.Configuration
{
    internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Rating)
                .HasConversion(x => x.Value, v => new Rating(v));

            builder.Property(x => x.Comment)
                .HasConversion(x => x.Value, v => new Comment(v))
                .HasMaxLength(1000);
        }
    }
}
