using System.Linq.Expressions;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Core.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id);
    Task<IEnumerable<Order?>> GetAllAsync(int pageNumber, int pageSize, Expression<Func<Order, bool>>? filter = null,
        Func<IQueryable<Order>, IOrderedQueryable<Order>>? orderBy = null);
    Task AddAsync(Order? order);
    Task UpdateAsync(Order? order);
    Task DeleteAsync(Guid id);
    Task<bool> AnyAsync(Expression<Func<Order, bool>> predicate, Guid productId);
}