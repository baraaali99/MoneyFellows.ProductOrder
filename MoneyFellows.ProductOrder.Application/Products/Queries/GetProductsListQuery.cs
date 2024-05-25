using AutoMapper;
using MediatR;
using MoneyFellows.ProductOrder.Application.Products.Dtos;
using MoneyFellows.ProductOrder.Core.Interfaces;

namespace MoneyFellows.ProductOrder.Application.Products.Queries
{
    public enum ProductsSortBy
    {
        Id,
        Price,
        ProductName,
        Merchant,
    }

    public class GetProductsListQuery : IRequest<GetProductsListQueryOutputDto>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public decimal? MinPrice { get; set; } = null;
        public decimal? MaxPrice { get; set; } = null;
        public string? Merchant { get; set; } = null;
        public ProductsSortBy SortBy { get; set; } = ProductsSortBy.Id;
        public bool SortAscending { get; set; } = true;
        public string? SearchTerm { get; set; } = null;
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
            var products = await _productRepository.GetAllAsync(
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                minPrice: request.MinPrice,
                maxPrice: request.MaxPrice,
                merchant: request.Merchant,
                sortBy: request.SortBy.ToString(),
                sortAscending: request.SortAscending,
                searchTerm: request.SearchTerm);

            var productsDto = _mapper.Map<IEnumerable<GetProductsListQueryOutputDtoItem>>(products);

            return new GetProductsListQueryOutputDto
            {
                Items = productsDto.ToList(),
            };
        }
    }
}