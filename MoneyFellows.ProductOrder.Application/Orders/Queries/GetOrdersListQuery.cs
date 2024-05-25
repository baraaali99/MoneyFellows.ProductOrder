using AutoMapper;
using MediatR;
using MoneyFellows.ProductOrder.Application.Orders.Dtos;
using MoneyFellows.ProductOrder.Core.Interfaces;

namespace MoneyFellows.ProductOrder.Application.Orders.Queries;

public enum OrdersSortBy
{
    Id,
    TotalCost,
    DeliveryTime,
}

public class GetOrdersListQuery : IRequest<GetOrdersListQueryOutputDto>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public OrdersSortBy SortBy { get; set; } = OrdersSortBy.Id;
    public bool SortAscending { get; set; } = true;
    public string? SearchTerm { get; set; } = null;
}

public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, GetOrdersListQueryOutputDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<GetOrdersListQueryOutputDto> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllAsync(pageNumber: request.PageNumber,
            pageSize: request.PageSize,
            sortBy: request.SortBy.ToString(),
            sortAscending: request.SortAscending,
            searchTerm: request.SearchTerm);

        var ordersDto = _mapper.Map<IEnumerable<GetOrdersListQueryOutputDtoItem>>(orders);
        return new GetOrdersListQueryOutputDto
        {
            Items = ordersDto.ToList()
        };
    }
}