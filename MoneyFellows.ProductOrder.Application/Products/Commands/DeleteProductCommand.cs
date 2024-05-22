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

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            throw new Exception("Product with Id: " + request.Id + " doesn't exist");
        }

        await _productRepository.DeleteAsync(request.Id);
        return Unit.Value;
    }
}