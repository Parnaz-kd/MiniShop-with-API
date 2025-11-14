using Microsoft.EntityFrameworkCore;
using MiniShop.Api.Domain.Entities;

namespace MiniShop.Api.Infrastructure.Persistence
{
    public class MiniShopDbContext : DbContext
    {
        public MiniShopDbContext(DbContextOptions<MiniShopDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(150);
            modelBuilder.Entity<Product>().HasIndex(p => p.Name).IsUnique(false);
            modelBuilder.Entity<Order>().OwnsMany(o=>o.Items);
        }
    }

}
