using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

namespace WebApi.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p=>p.Name).HasMaxLength(50).IsRequired(true);
            builder.Property(p=>p.SalePrice).IsRequired(true);
            builder.Property(p=>p.CreatedDate).HasDefaultValue(DateTime.UtcNow);
            builder.Property(p => p.UpdatedDate).HasDefaultValue(DateTime.UtcNow);

        }
    }
}
