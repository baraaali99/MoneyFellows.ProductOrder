using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Core.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id);
    Task<IEnumerable<Order?>> GetAllAsync(int pageNumber, int pageSize);
    Task AddAsync(Order? order);
    Task UpdateAsync(Order? order);
    Task DeleteAsync(Guid id);
}