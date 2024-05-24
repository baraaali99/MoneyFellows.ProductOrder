using MediatR;
using MoneyFellows.ProductOrder.Core.Interfaces;

namespace MoneyFellows.ProductOrder.Application.Products.Commands;

public class DeleteProductCommand : IRequest
{
    public Guid Id { get; set; }
    
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    public DeleteProductCommandHandler(IProductRepository productRepository, IOrderRepository orderRepository)
    {
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            throw new Exception("Product with Id: " + request.Id + " doesn't exist");
        }
        var isProductInPastOrders = await _orderRepository.AnyAsync(o => o.DeliveryTime < DateTime.Now && o.OrderDetails.Any(od => od.ProductId == request.Id), request.Id);
        if (isProductInPastOrders)
        {
            throw new Exception("Product with Id: " + request.Id + " cannot be deleted because it is associated with past orders.");
        }

        await _productRepository.DeleteAsync(request.Id);
        return Unit.Value;
    }
}