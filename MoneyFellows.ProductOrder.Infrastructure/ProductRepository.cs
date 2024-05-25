using System.Globalization;
using System.Linq.Expressions;
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
    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize,
        decimal? minPrice, decimal? maxPrice, string? merchant,
        string sortBy, bool sortAscending, string? searchTerm)
    {
        var query = _dbContext.Products.AsQueryable();

        if (minPrice.HasValue)
        {
            query = query.Where(p => p.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= maxPrice.Value);
        }

        if (!string.IsNullOrEmpty(merchant))
        {
            query = query.Where(p => p.Merchant.ToLower().Contains(merchant.Trim().ToLower()));
        }

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(p => p.ProductName.ToLower().Contains(searchTerm.Trim().ToLower())
            || p.ProductDescription.ToLower().Contains(searchTerm.Trim().ToLower()));
        }

        if (!string.IsNullOrEmpty(sortBy))
        {
            if (sortAscending)
            {
                query = query.OrderBy(p => EF.Property<object>(p, sortBy));
            }
            else
            {
                query = query.OrderByDescending(p => EF.Property<object>(p, sortBy));
            }
        }

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
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

    public async Task DeleteAsync(int id)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product != null)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> IsProductExistsAsync(string productName)
    {
        return await _dbContext.Products.AnyAsync(p => p.ProductName.ToLower().Trim() == productName.ToLower().Trim());
    }

    public async Task<bool> AreProductsExistAsync(List<int> productsIds)
    {
        var existingCount = await _dbContext.Products.CountAsync(p => productsIds.Contains(p.Id));
        return existingCount == productsIds.Count;
    }

    public async Task<bool> IsProductOrderedBeforeAsync(int productId)
    {
        return await _dbContext.OrderDetails.AnyAsync(od => od.ProductId == productId);
    }
}