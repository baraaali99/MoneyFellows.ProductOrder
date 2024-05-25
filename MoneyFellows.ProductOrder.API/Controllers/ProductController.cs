using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyFellows.ProductOrder.Application.Products.Commands;
using MoneyFellows.ProductOrder.Application.Products.Queries;

namespace MoneyFellows.ProductOrder.API.Controllers;
[ApiController]
[Route("api/v{version:apiVersion}/products")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductController> _logger;
    public ProductController(IMediator mediator, ILogger<ProductController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ApiVersion("1")]
    public async Task<IActionResult> GetProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
    {
        _logger.LogInformation("Fetching products with pageNumber: {pageNumber}, pageSize: {pageSize}", pageNumber, pageSize);

        var getProductListQuery = new GetProductsListQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
        };
        var products = await _mediator.Send(getProductListQuery);
        _logger.LogInformation("Fetched {productCount} products", products.Items.Count());
        return Ok(products);
    }

    [HttpPost]
    [ApiVersion("1")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand createProductCommand)
    {
        _logger.LogInformation("Creating a new product");
        await _mediator.Send(createProductCommand);
        _logger.LogInformation("Product created successfully");
        return Ok();
    }

    [HttpGet("{id}")]
    [ApiVersion("1")]
    public async Task<IActionResult> GetProductById(int id)
    {
        _logger.LogInformation("Getting product by id: {id}", id);
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        if (product == null)
        {
            _logger.LogWarning("Product with id: {id} not found", id);
            return NotFound();
        }
        _logger.LogInformation("Product with id: {id} fetched successfully", id);
        return Ok(product);
    }

    [HttpPut("{id}")]
    [ApiVersion("1")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductCommand updateProductCommand)
    {
        _logger.LogInformation("Updating product with id: {id}", id);
        updateProductCommand.Id = id;
        await _mediator.Send(updateProductCommand);
        _logger.LogInformation("Product with id: {id} updated successfully", id);
        return Ok();
    }

    [HttpDelete("{id}")]
    [ApiVersion("2")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        _logger.LogInformation("Deleting product with id: {id}", id);
        var deleteProductCommand = new DeleteProductCommand()
        {
            Id = id
        };

        await _mediator.Send(deleteProductCommand);
        _logger.LogInformation("Product with id: {id} deleted successfully", id);
        return Ok();
    }
}