using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyFellows.ProductOrder.Application.Orders.Commands;
using MoneyFellows.ProductOrder.Application.Orders.Queries;
using Serilog;

namespace MoneyFellows.ProductOrder.API.Controllers;
[ApiController]
[Route("api/v{version:apiVersion}/orders")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ApiVersion("1")]
    public async Task<IActionResult> GetOrders([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
    {
        Log.Information("Fetching orders with pageNumber: {pageNumber}, pageSize: {pageSize}", pageNumber, pageSize);
        var getOrdersQueryList = new GetOrdersListQuery()
        {
            pageNumber = pageNumber,
            pageSize = pageSize,
        };
        var orders = await _mediator.Send(getOrdersQueryList);
        Log.Information("Fetched {orderCount} orders", orders.Items.Count());
        return Ok(orders);
    }

    [HttpGet("{id}")]
    [ApiVersion("1")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        Log.Information("Getting order by id: {id}", id);
        var order = await _mediator.Send(new GetOrderByIdQuery(id));
        if (order == null)
        {
            Log.Error("Order with id: {id} not found", id);
            return NotFound();
        }
        Log.Information("Order with id: {id} fetched successfully", id);
        return Ok(order);
    }

    [HttpPost]
    [ApiVersion("1")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand createOrderCommand)
    {
        Log.Information("Creating a new order");
        await _mediator.Send(createOrderCommand);
        Log.Information("Order created successfully");
        return Ok();
    }
    [HttpDelete("{id}")]
    [ApiVersion("1")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        Log.Information("Deleting order with id : {id}", id);
        var deleteOrderCommand = new DeleteOrderCommand()
        {
            id = id
        };

        await _mediator.Send(deleteOrderCommand);
        Log.Information("Order deleted with id: {id}", id);
        return Ok();
    }

    [HttpPut("{id}")]
    [ApiVersion("1")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateOrderCommand updateOrderCommand)
    {
        Log.Information("Updating order with id : {id}", id);
        updateOrderCommand.Id = id;
        await _mediator.Send(updateOrderCommand);
        Log.Information("Order updated with id : {id}", id);
        return Ok();
    }
}