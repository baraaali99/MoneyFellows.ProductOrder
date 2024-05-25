using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Infrastructure.Data;

public class ProductOrderDbContext : DbContext
{
    public ProductOrderDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public ProductOrderDbContext()
    {
        // for scaffolding
    }
    public  DbSet<Product> Products { get; set; }
    public  DbSet<Order?> Orders { get; set; }
    public DbSet<OrderDetails> orderDetails { get; set; }
    public DbSet<CustomerDetails> CustomerDetailsEnumerable { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Product configuration
        modelBuilder.Entity<Product>()
            .HasIndex(p => new { p.ProductName })
            .IsUnique(true);
        
        modelBuilder.Entity<OrderDetails>()
            .HasOne(od => od.Product)
            .WithMany()
            .HasForeignKey(od => od.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderDetails>()
            .HasIndex(od => new { od.ProductId, od.OrderId })
            .IsUnique(true);
    }
    
}