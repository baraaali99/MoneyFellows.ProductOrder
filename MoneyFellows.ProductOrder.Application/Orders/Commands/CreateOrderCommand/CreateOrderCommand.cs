using AutoMapper;
using MediatR;
using MoneyFellows.ProductOrder.Core.Interfaces;
using MoneyFellows.ProductOrder.Core.Models;

namespace MoneyFellows.ProductOrder.Application.Orders.Commands.CreateOrderCommand;

public class CreateOrderCommand : IRequest
{
    public string DeliveryAddress { get; set; }
    public decimal TotalCost { get; set; }
    public List<CreateOrderCommandOrderDetail> OrderDetails { get; set; }
    public CreateOrderCommandCustomer Customer { get; set; }
    public DateTime DeliveryTime { get; set; }
}

public class CreateOrderCommandCustomer
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string ContactNumber { get; set; }
}

public class CreateOrderCommandOrderDetail
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IOrderRepository orderRepository,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if (!await _productRepository.AreProductsExistAsync(request.OrderDetails.Select(od => od.ProductId).ToList()))
        {
            throw new Exception("One or more products do not exist.");
        }

        var order = _mapper.Map<Order>(request);

        await _orderRepository.AddAsync(order);

        return Unit.Value;
    }
}