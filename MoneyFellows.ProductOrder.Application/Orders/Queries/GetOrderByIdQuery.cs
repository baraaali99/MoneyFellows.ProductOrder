using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MoneyFellows.ProductOrder.Application.Orders.Dtos;
using MoneyFellows.ProductOrder.Core.Interfaces;
using Serilog;

namespace MoneyFellows.ProductOrder.Application.Orders.Queries;

public class GetOrderByIdQuery : IRequest<GetOrderbyIdQueryDto>
{
    public int Id { get; set; }

    public GetOrderByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, GetOrderbyIdQueryDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    public async Task<GetOrderbyIdQueryDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id);
        if (order == null)
        {
            Log.Error("Order with Id: {OrderId} not found", request.Id);
            throw new Exception("Order with Id: " + request.Id +" not found");
        }

        var orderDto = _mapper.Map<GetOrderbyIdQueryDto>(order);
        return orderDto;
    }
}