using Microsoft.EntityFrameworkCore;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Infrastructure.Data;

public class ProductOrderDbContext : DbContext
{
    public ProductOrderDbContext(DbContextOptions<ProductOrderDbContext> options) : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
}