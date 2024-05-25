using AutoMapper;
using MediatR;
using MoneyFellows.ProductOrder.Core.Interfaces;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Application.Products.Commands.CreateProductCommand;

public class CreateProductCommand : IRequest
{
    public string ProductName { get; set; } = string.Empty;

    public string ProductDescription { get; set; } = string.Empty;

    public byte[]? ProductImage { get; set; } = null;

    public decimal Price { get; set; }

    public string Merchant { get; set; } = string.Empty;
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (await _productRepository.IsProductExistsAsync(request.ProductName))
        {
            throw new Exception($"Product with name {request.ProductName} already exists");
        }

        var product = _mapper.Map<Product>(request);
        await _productRepository.AddAsync(product);
        return Unit.Value;
    }
}