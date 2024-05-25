using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MoneyFellows.ProductOrder.Core.Interfaces;
using Serilog;

namespace MoneyFellows.ProductOrder.Application.Products.Commands.UpdateProductCommand;

public class UpdateProductCommand : IRequest
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductDescription { get; set; } = string.Empty;

    public byte[]? ProductImage { get; set; }

    public decimal Price { get; set; }

    public string Merchant { get; set; } = string.Empty;
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    { 
        Log.Information("Attempting to update product with Id: {ProductId}", request.Id);
        var product = await _productRepository.GetByIdAsync(request.Id);

        if (product == null)
        {
            Log.Error("Product with Id: {ProductId} not found", request.Id);
            throw new Exception("can't find Product with Id = " + request.Id);
        }

        if (product.ProductName != request.ProductName && await _productRepository.IsProductExistsAsync(request.ProductName))
        {
            Log.Error("Product with name: {ProductName} already exists", request.ProductName);
            throw new Exception("Product with name: " + request.ProductName + " already exists");
        }

        _mapper.Map(request, product);
        await _productRepository.UpdateAsync(product);
        return Unit.Value;
    }
}