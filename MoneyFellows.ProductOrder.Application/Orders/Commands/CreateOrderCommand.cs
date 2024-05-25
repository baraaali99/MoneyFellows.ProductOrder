using AutoMapper;
using MediatR;
using MoneyFellows.ProductOrder.Application.Orders.Dtos;
using MoneyFellows.ProductOrder.Core.Interfaces;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Application.Orders.Commands;

public class CreateOrderCommand : IRequest
{
    public string DeliveryAddress { get; set; }
    public decimal TotalCost { get; set; }
    public List<CommandsOrderDetailsDto> OrderDetails { get; set; }
    public Customer CustomerDetails { get; set; }
    public DateTime DeliveryTime { get; set; }
}

public class OrderDetails
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(request);
        foreach (var detail in request.OrderDetails)
        {
            _mapper.Map<OrderDetails>(request.OrderDetails);
        }

        await _orderRepository.AddAsync(order);
        return Unit.Value;
    }
}