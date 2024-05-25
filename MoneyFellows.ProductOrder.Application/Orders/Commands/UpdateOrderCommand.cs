using AutoMapper;
using FluentValidation;
using MediatR;
using MoneyFellows.ProductOrder.Application.Orders.Dtos;
using MoneyFellows.ProductOrder.Core.Models;
using MoneyFellows.ProductOrder.Core.Interfaces;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Application.Orders.Commands;

public class UpdateOrderCommand : IRequest
{
    public Guid Id { get; set; }
    public string DeliveryAddress { get; set; }
    public decimal TotalCost { get; set; }
    public List<CommandsOrderDetailsDto> OrderDetails { get; set; }
    public CustomerDetails CustomerDetails { get; set; }
    public DateTime DeliveryTime { get; set; }
}

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id);
        if (order == null)
        {
            throw new Exception("order doesn't exist");
        }
        ValidateDeliveryTime(order, request);
        _mapper.Map(request, order);
        await _orderRepository.UpdateAsync(order);
        return Unit.Value;
    }
    private void ValidateDeliveryTime(Order originalOrder, UpdateOrderCommand updateOrderCommand)
    {
        if (updateOrderCommand.DeliveryTime < originalOrder.DeliveryTime)
        {
            throw new ValidationException("Delivery Time cannot be set to a time earlier than the original delivery time.");
        }
    }
}