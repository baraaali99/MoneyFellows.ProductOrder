using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MoneyFellows.ProductOrder.Infrastructure.Data
{
    public class ProductOrderDbContextFactory : IDesignTimeDbContextFactory<ProductOrderDbContext>
    {
        public ProductOrderDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductOrderDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=MoneyFellows_ProductOrder;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");

            return new ProductOrderDbContext(optionsBuilder.Options);
        }
    }
}