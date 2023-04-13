using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

namespace WebApi.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired(true);
            builder.Property(p => p.Desc).HasMaxLength(400).IsRequired(false);
            builder.Property(p => p.CreatedDate).HasDefaultValue(DateTime.UtcNow);
            builder.Property(p => p.UpdatedDate).HasDefaultValue(DateTime.UtcNow);
        }
    }
}
