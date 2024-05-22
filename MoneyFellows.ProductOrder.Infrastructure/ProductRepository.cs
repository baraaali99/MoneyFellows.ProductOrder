using Microsoft.EntityFrameworkCore;
using MoneyFellows.ProductOrder.Core.Interfaces;
using MoneyFellows.ProductOrder.Core.Models;
using MoneyFellows.ProductOrder.Infrastructure.Data;

namespace MoneyFellows.ProductOrder.Infrastructure;

public class ProductRepository : IProductRepository
{
    private readonly ProductOrderDbContext _dbContext;

    public ProductRepository(ProductOrderDbContext _dbContext)
    {
        this._dbContext = _dbContext;
    }
    public async Task<Product> GetByIdAsync(Guid id)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await _dbContext.Products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product != null)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}