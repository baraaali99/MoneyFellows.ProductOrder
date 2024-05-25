using MediatR;
using MoneyFellows.ProductOrder.Core.Interfaces;

namespace MoneyFellows.ProductOrder.Application.Orders.Commands.DeleteOrderCommand;

public class DeleteOrderCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id) ?? throw new Exception("Order with Id: " + request.Id + " doesn't exist");
        if (order.DeliveryTime < DateTime.Now)
        {
            throw new InvalidOperationException("cannot delete order with past deliver time");
        }

        await _orderRepository.DeleteAsync(order.Id);
        return Unit.Value;
    }
}