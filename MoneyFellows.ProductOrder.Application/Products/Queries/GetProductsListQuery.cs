using AutoMapper;
using MediatR;
using MoneyFellows.ProductOrder.Application.Products.Dtos;
using MoneyFellows.ProductOrder.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Application.Products.Queries
{
    public class GetProductsListQuery : IRequest<GetProductsListQueryOutputDto>
    {    
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string Merchant { get; set; }
        public string SortBy { get; set; }
        public bool SortAscending { get; set; } = true;
    }

    public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, GetProductsListQueryOutputDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        
        public GetProductsListQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GetProductsListQueryOutputDto> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(request.PageNumber, request.PageSize);
            var productsDto = _mapper.Map<IEnumerable<GetProductsListQueryOutputDtoItem>>(products);
            return new GetProductsListQueryOutputDto
            {
                Items = productsDto.ToList(),
            };
        }
    }
}