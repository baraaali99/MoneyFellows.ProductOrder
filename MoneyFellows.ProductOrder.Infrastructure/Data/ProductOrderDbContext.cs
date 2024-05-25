using Microsoft.EntityFrameworkCore;
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

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Customer> Customers { get; set; }
}