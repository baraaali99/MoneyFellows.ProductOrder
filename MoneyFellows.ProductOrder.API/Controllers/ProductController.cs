using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyFellows.ProductOrder.Application.Products.Commands;
using MoneyFellows.ProductOrder.Application.Products.Commands.CreateProductCommand;
using MoneyFellows.ProductOrder.Application.Products.Commands.DeleteProductCommand;
using MoneyFellows.ProductOrder.Application.Products.Commands.UpdateProductCommand;
using MoneyFellows.ProductOrder.Application.Products.Queries;
using Serilog;

namespace MoneyFellows.ProductOrder.API.Controllers;
[ApiController]
[Route("api/v{version:apiVersion}/products")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ApiVersion("1")]
    public async Task<IActionResult> GetProducts([FromQuery] GetProductsListQuery getProductsListQuery)
    {
        
        Log.Information("Fetching products with pageNumber: {pageNumber}, pageSize: {pageSize}", getProductsListQuery.PageNumber, getProductsListQuery.PageSize);
        var products = await _mediator.Send(getProductsListQuery);
        Log.Information("Fetched {productCount} products", products.Items.Count());
        return Ok(products);
    }

    [HttpPost]
    [ApiVersion("1")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand createProductCommand)
    {
        Log.Information("Creating a new product");
        await _mediator.Send(createProductCommand);
        Log.Information("Product created successfully");
        return Ok();
    }

    [HttpGet("{id}")]
    [ApiVersion("1")]
    public async Task<IActionResult> GetProductById(int id)
    {
        Log.Information("Getting product by id: {id}", id);
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        if (product == null)
        {
            Log.Error("Product with id: {id} not found", id);
            return NotFound();
        } 
        Log.Information("Product with id: {id} fetched successfully", id);
        return Ok(product);
    }

    [ApiVersion("1")]
    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand updateProductCommand)
    {
        Log.Information("Updating product with id: {id}", updateProductCommand.Id);
        await _mediator.Send(updateProductCommand);
        Log.Information("Product with id: {id} updated successfully", updateProductCommand.Id);
        return Ok();
    }

    [HttpDelete("{id}")]
    [ApiVersion("2")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        Log.Information("Deleting product with id: {id}", id);
        var deleteProductCommand = new DeleteProductCommand()
        {
            Id = id
        };

        await _mediator.Send(deleteProductCommand);
        Log.Information("Product with id: {id} deleted successfully", id);
        return Ok();
    }
}