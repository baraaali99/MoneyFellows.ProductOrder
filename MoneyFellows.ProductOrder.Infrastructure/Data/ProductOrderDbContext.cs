using Microsoft.EntityFrameworkCore;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Infrastructure.Data;

public class ProductOrderDbContext : DbContext
{
    public ProductOrderDbContext(DbContextOptions<ProductOrderDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasIndex(p => new { p.ProductName })
            .IsUnique(true);
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
}