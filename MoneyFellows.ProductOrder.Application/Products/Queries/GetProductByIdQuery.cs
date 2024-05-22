using AutoMapper;
using MediatR;
using MoneyFellows.ProductOrder.Application.DTOs;
using MoneyFellows.ProductOrder.Core.Interfaces;

namespace MoneyFellows.ProductOrder.Application.Products.Queries;

public class GetProductByIdQuery : IRequest<ProductDTO>
{
    public Guid Id { get;}

    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        var productDto = _mapper.Map<ProductDTO>(product);
        return productDto;
    }
}