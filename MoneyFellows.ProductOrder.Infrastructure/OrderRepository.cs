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

    public async Task<IEnumerable<Order>> GetAllAsync(int pageNumber, int pageSize,
        string sortBy, bool sortAscending, string? searchTerm)
    {
        var query = _dbContext.Orders.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(o => o.DeliveryAddress.ToLower().Contains(searchTerm.Trim().ToLower())
            || o.Customer.Name.ToLower().Contains(searchTerm.Trim().ToLower()));
        }

        if (!string.IsNullOrEmpty(sortBy))
        {
            if (sortAscending)
            {
                query = query.OrderBy(o => EF.Property<object>(o, sortBy));
            }
            else
            {
                query = query.OrderByDescending(o => EF.Property<object>(o, sortBy));
            }
        }

        return await query
            .Include(o => o.OrderDetails)
            .Include(c => c.Customer)
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
}