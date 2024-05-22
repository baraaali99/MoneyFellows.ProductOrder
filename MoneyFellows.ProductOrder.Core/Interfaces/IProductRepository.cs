using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Core.Interfaces;

public interface IProductRepository
{   
    Task<Product> GetByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Guid id);
}