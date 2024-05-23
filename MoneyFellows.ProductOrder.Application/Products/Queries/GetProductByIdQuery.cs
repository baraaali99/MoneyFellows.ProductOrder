using AutoMapper;
using MediatR;
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

    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<GetProductByIdQueryDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        var productDto = _mapper.Map<GetProductByIdQueryDto>(product);
        return productDto;
    }
}