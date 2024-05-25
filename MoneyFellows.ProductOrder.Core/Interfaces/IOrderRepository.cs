using System.Linq.Expressions;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Core.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int id);
    Task<IEnumerable<Order?>> GetAllAsync(int pageNumber, int pageSize);
    Task AddAsync(Order? order);
    Task UpdateAsync(Order? order);
    Task DeleteAsync(int id);
    Task<bool> AnyAsync(Expression<Func<Order, bool>> predicate, int productId);
}