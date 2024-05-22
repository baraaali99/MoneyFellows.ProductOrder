using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyFellows.ProductOrder.Application.DTOs;
using MoneyFellows.ProductOrder.Application.IServices;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.API.Controllers;
[ApiController]
[Route("api/v1/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult>GetProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
        return Ok(productsDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        var productDto = _mapper.Map<ProductDTO>(product);
        return Ok(productDto);
    }
    
}