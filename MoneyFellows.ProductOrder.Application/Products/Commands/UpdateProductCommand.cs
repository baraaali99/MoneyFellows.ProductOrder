using AutoMapper;
using MediatR;
using MoneyFellows.ProductOrder.Core.Interfaces;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Application.Products.Commands;

public class UpdateProductCommand : IRequest
{
    public Guid Id { get; set; }
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
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            throw new Exception("can't find Product with Id = " + request.Id);
        }
        _mapper.Map(request, product);
        product.Id = request.Id;
        await _productRepository.UpdateAsync(product);
        return Unit.Value;
    }
}