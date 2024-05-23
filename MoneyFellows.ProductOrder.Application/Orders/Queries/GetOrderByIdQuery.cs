using AutoMapper;
using MediatR;
using MoneyFellows.ProductOrder.Application.Orders.Dtos;
using MoneyFellows.ProductOrder.Core.Interfaces;

namespace MoneyFellows.ProductOrder.Application.Orders.Queries;

public class GetOrderByIdQuery : IRequest<GetOrderbyIdQueryDto>
{
    public Guid Id { get; set; }

    public GetOrderByIdQuery(Guid id)
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
            throw new Exception("Order Not Found");
        }

        var orderDto = _mapper.Map<GetOrderbyIdQueryDto>(order);
        return orderDto;
    }
}