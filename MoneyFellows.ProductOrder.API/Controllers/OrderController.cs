using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyFellows.ProductOrder.Application.Orders.Commands;
using MoneyFellows.ProductOrder.Application.Orders.Queries;

namespace MoneyFellows.ProductOrder.API.Controllers;
[ApiController]
[Route("api/v{version:apiVersion}/orders")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<OrderController> _logger;
    public OrderController(IMediator mediator, ILogger<OrderController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ApiVersion("1")]
    public async Task<IActionResult> GetOrders([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
    {
        _logger.LogInformation("Fetching orders with pageNumber: {pageNumber}, pageSize: {pageSize}", pageNumber, pageSize);
        var getOrdersQueryList = new GetOrdersListQuery()
        {
            pageNumber = pageNumber,
            pageSize = pageSize,
        };
        var orders = await _mediator.Send(getOrdersQueryList);
        _logger.LogInformation("Fetched {orderCount} orders", orders.Items.Count());
        return Ok(orders);
    }

    [HttpGet("{id}")]
    [ApiVersion("1")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        _logger.LogInformation("Getting order by id: {id}", id);
        var order = await _mediator.Send(new GetOrderByIdQuery(id));
        if (order == null)
        {
            _logger.LogWarning("Order with id: {id} not found", id);
            return NotFound();
        }
        _logger.LogInformation("Order with id: {id} fetched successfully", id);
        return Ok(order);
    }

    [HttpPost]
    [ApiVersion("1")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand createOrderCommand)
    {
        _logger.LogInformation("Creating a new order");
        await _mediator.Send(createOrderCommand);
        _logger.LogInformation("Order created successfully");
        return Ok();
    }
    [HttpDelete("{id}")]
    [ApiVersion("1")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        _logger.LogInformation("Deleting order with id : {id}", id);
        var deleteOrderCommand = new DeleteOrderCommand()
        {
            id = id
        };

        await _mediator.Send(deleteOrderCommand);
        _logger.LogInformation("Order deleted with id: {id}", id);
        return Ok();
    }

    [HttpPut("{id}")]
    [ApiVersion("1")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateOrderCommand updateOrderCommand)
    {
        _logger.LogInformation("Updating order with id : {id}", id);
        updateOrderCommand.Id = id;
        await _mediator.Send(updateOrderCommand);
        _logger.LogInformation("Order updated with id : {id}", id);
        return Ok();
    }
}