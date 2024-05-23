using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyFellows.ProductOrder.Application.Orders.Queries;

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
    public async Task<IActionResult> GetOrders([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
    {
        var getOrdersQueryList = new GetOrdersListQuery()
        {
            pageNumber = pageNumber,
            pageSize = pageSize
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
}