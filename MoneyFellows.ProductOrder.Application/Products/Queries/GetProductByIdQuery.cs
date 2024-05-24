using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MoneyFellows.ProductOrder.Application.Products.Dtos;
using MoneyFellows.ProductOrder.Core.Interfaces;

namespace MoneyFellows.ProductOrder.Application.Products.Queries;

public class GetProductByIdQuery : IRequest<GetProductByIdQueryDto>
{
    public Guid Id { get;}

    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetProductByIdQueryHandler> _logger;
    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper, ILogger<GetProductByIdQueryHandler> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<GetProductByIdQueryDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            _logger.LogWarning("Product with Id: {ProductId} not found", request.Id);
            throw new Exception("Product with Id: " + request.Id +" not found");
        }
        var productDto = _mapper.Map<GetProductByIdQueryDto>(product);
        return productDto;
    }
}