using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MoneyFellows.ProductOrder.Application.Products.Dtos;
using MoneyFellows.ProductOrder.Core.Interfaces;
using Serilog;

namespace MoneyFellows.ProductOrder.Application.Products.Queries;

public class GetProductByIdQuery : IRequest<GetProductByIdQueryDto>
{
    public int Id { get;}

    public GetProductByIdQuery(int id)
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
        if (product == null)
        {
            throw new Exception("Product with Id: " + request.Id +" not found");
        }
        var productDto = _mapper.Map<GetProductByIdQueryDto>(product);
        return productDto;
    }
}