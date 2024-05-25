using System.Linq.Expressions;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Core.Interfaces;

public interface IProductRepository
{   
    Task<Product?> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize,
        decimal? minPrice, decimal? maxPrice, string? merchant,
        string sortBy, bool sortAscending, string? searchTerm);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
    Task<bool> IsProductExistsAsync(string productName);
    Task<bool> IsProductOrderedBeforeAsync(int productId);
}