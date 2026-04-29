using Microsoft.EntityFrameworkCore;
using Shopnest.Api.Models;

namespace Shopnest.Api.Data
{
    public class StoreContext: DbContext
    {
        public StoreContext(DbContextOptions options) : base(options) { }

        // This creates the "Products" table in SQL Server
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // This tells SQL: "Store Price with 18 digits total, 2 after the decimal"
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
