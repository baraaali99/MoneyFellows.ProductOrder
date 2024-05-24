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
    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Orders.Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Order?>> GetAllAsync(int pageNumber, int pageSize, Expression<Func<Order, bool>>? filter = null, 
        Func<IQueryable<Order>, IOrderedQueryable<Order>>? orderBy = null )
    {
        IQueryable<Order> query = _dbContext.Orders.Include(o => o.OrderDetails);
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }
        return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
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

    public async Task DeleteAsync(Guid id)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if (order != null)
        {
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}