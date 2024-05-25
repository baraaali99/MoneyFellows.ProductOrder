using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MoneyFellows.ProductOrder.Core.Interfaces;
using MoneyFellows.ProductOrder.Core.Models;
using MoneyFellows.ProductOrder.Infrastructure.Data;

namespace MoneyFellows.ProductOrder.Infrastructure;

public class OrderRepository : IOrderRepository
{
    public ProductOrderDbContext _dbContext;
    public OrderRepository(ProductOrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _dbContext.Orders.
            Include(o => o.OrderDetails).
            Include(c => c.Customer)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Order?>> GetAllAsync(int pageNumber, int pageSize )
    {
        return await _dbContext.Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .Include(o => o.Customer)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task AddAsync(Order? order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order? order)
    {
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if (order != null)
        {
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> AnyAsync(Expression<Func<Order, bool>> predicate, int productId)
    {
        return await _dbContext.Orders
            .Where(predicate)
            .SelectMany(o => o.OrderDetails) // Flatten order details
            .AnyAsync(od => od.ProductId == productId); // Check if any order detail has the specified product Id
    }
}