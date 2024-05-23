using Microsoft.EntityFrameworkCore;
using MoneyFellows.ProductOrder.Core.Interfaces;
using MoneyFellows.ProductOrder.Core.Models;
using MoneyFellows.ProductOrder.Infrastructure.Data;

namespace MoneyFellows.ProductOrder.Infrastructure;

public class OrderRepository : IOrderRepository
{
    public ProductOrderDbContext _DbContext;
    public OrderRepository(ProductOrderDbContext _dbContext)
    {
        _DbContext = _dbContext;
    }
    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _DbContext.Orders.Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Order?>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await _DbContext.Orders.Include(o => o.OrderDetails)
            .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task AddAsync(Order? order)
    {
        await _DbContext.Orders.AddAsync(order);
        await _DbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order? order)
    {
        _DbContext.Orders.Update(order);
        await _DbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var order = await _DbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if (order != null)
        {
            _DbContext.Orders.Remove(order);
            await _DbContext.SaveChangesAsync();
        }
    }
}