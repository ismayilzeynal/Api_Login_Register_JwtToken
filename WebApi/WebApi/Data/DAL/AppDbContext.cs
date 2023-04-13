using Microsoft.EntityFrameworkCore;
using WebApi.Data.Configuration;
using WebApi.Models;

namespace WebApi.Data.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());



            // search modelBuilder.ApplyConfigurationsFromAssembly 
            base.OnModelCreating(modelBuilder);
        }
    }
}
