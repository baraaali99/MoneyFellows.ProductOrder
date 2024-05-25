using System.Linq.Expressions;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Core.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int id);
    Task<IEnumerable<Order>> GetAllAsync(int pageNumber, int pageSize, string sortBy,
        bool sortAscending, string? searchTerm);
    Task AddAsync(Order? order);
    Task UpdateAsync(Order? order);
    Task DeleteAsync(int id);
}