using MediatR;
using MoneyFellows.ProductOrder.Core.Interfaces;

namespace MoneyFellows.ProductOrder.Application.Orders.Commands;

public class DeleteOrderCommand : IRequest
{
    public Guid id { get; set; }
}

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private IOrderRepository _orderRepository;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.id);
        if (order == null)
        {
            throw new Exception("Order with Id: " + request.id + " doesn't exist");
        }

        if (order.DeliveryTime < DateTime.Now)
        {
            throw new InvalidOperationException("cannot delete order with past deliver time");
        }

        await _orderRepository.DeleteAsync(order.Id);
        return Unit.Value;
    }
}