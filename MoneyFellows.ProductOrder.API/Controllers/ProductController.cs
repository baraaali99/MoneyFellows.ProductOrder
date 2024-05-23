using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyFellows.ProductOrder.Application.IServices;
using MoneyFellows.ProductOrder.Application.Products.Commands;
using MoneyFellows.ProductOrder.Application.Products.Queries;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.API.Controllers;
[ApiController]
[Route("api/v1/products")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult>GetProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
    {
        var getProductListQuery = new GetProductsListQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var products = await _mediator.Send(getProductListQuery);
        return Ok(products);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand createProductCommand)
    {
        await _mediator.Send(createProductCommand);
        return Ok();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductCommand updateProductCommand)
    {
        updateProductCommand.Id = id;
        await _mediator.Send(updateProductCommand);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var deleteProductCommand = new DeleteProductCommand()
        {
            Id = id
        };
        await _mediator.Send(deleteProductCommand);
        return Ok();
    }
}