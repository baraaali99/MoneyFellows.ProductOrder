using System.Linq.Expressions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyFellows.ProductOrder.Application.Orders.Commands;
using MoneyFellows.ProductOrder.Application.Orders.Queries;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.API.Controllers;
[ApiController]
[Route("api/v1/orders")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20,
        [FromQuery]Expression<Func<Order, bool>>? filter = null,[FromQuery] Func<IQueryable<Order>, IOrderedQueryable<Order>>? orderBy = null)
    {
        var getOrdersQueryList = new GetOrdersListQuery()
        {
            pageNumber = pageNumber,
            pageSize = pageSize,
            filter = filter,
            orderBy = orderBy
        };
        var orders = await _mediator.Send(getOrdersQueryList);
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var order = await _mediator.Send(new GetOrderByIdQuery(id));
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand createOrderCommand)
    {
        await _mediator.Send(createOrderCommand);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var deleteOrderCommand = new DeleteOrderCommand()
        {
            id = id
        };
        
        await _mediator.Send(deleteOrderCommand);
        return Ok();
    }
}