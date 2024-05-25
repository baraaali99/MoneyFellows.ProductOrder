using AutoMapper;
using FluentValidation;
using MediatR;
using MoneyFellows.ProductOrder.Core.Interfaces;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Application.Orders.Commands.UpdateOrderCommand;

public class UpdateOrderCommand : IRequest
{
    public int Id { get; set; }
    public string DeliveryAddress { get; set; }
    public decimal TotalCost { get; set; }
    public List<UpdateOrderCommandOrderDetail> OrderDetails { get; set; }
    public UpdateOrderCommandCustomer Customer { get; set; }
    public DateTime DeliveryTime { get; set; }
}

public class UpdateOrderCommandCustomer
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string ContactNumber { get; set; }
}

public class UpdateOrderCommandOrderDetail
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id) ?? throw new Exception("order doesn't exist");

        if (!await _productRepository.AreProductsExistAsync(request.OrderDetails.Select(od => od.ProductId).ToList()))
            throw new Exception("One or more products do not exist.");

        ValidateDeliveryTime(order, request);

        _mapper.Map(request, order);

        await _orderRepository.UpdateAsync(order);
        return Unit.Value;
    }
    private static void ValidateDeliveryTime(Order originalOrder, UpdateOrderCommand updateOrderCommand)
    {
        if (updateOrderCommand.DeliveryTime < originalOrder.DeliveryTime)
        {
            throw new ValidationException("Delivery Time cannot be set to a time earlier than the original delivery time.");
        }
    }
}