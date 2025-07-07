using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStoreAPI.Domain.Categories.Entities;
using OnlineStoreAPI.Domain.Categories.ValueObjects;

namespace OnlineStore.Infrastructure.Configuration
{
    internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                    .HasMaxLength(200)
                   .HasConversion(name => name.Value, value => new CategoryName(value));

            builder.Property(c => c.Description)
                    .HasMaxLength(500)
                   .HasConversion(desc => desc.Value, value => new CategoryDescription(value));
        }
    }
}
