using System.Linq.Expressions;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Core.Interfaces;

public interface IProductRepository
{   
    Task<Product?> GetByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Guid id);
}