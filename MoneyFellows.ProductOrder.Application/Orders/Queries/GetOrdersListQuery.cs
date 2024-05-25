using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using MoneyFellows.ProductOrder.Application.Orders.Dtos;
using MoneyFellows.ProductOrder.Core.Interfaces;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Application.Orders.Queries;

public class GetOrdersListQuery : IRequest<GetOrdersListQueryOutputDto>
{
    public int pageNumber { get; set; }
    public int pageSize { get; set; }
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
        var orders = await _orderRepository.GetAllAsync(request.pageNumber, request.pageSize);
        var ordersDto = _mapper.Map<IEnumerable<GetOrdersListQueryOutputDtoItem>>(orders);
        return new GetOrdersListQueryOutputDto
        {
            Items = ordersDto.ToList()
        };
    }
}