using MediatR;
using Microsoft.Extensions.Logging;
using MoneyFellows.ProductOrder.Core.Interfaces;
using Serilog;

namespace MoneyFellows.ProductOrder.Application.Products.Commands.DeleteProductCommand;

public class DeleteProductCommand : IRequest
{
    public int Id { get; set; }

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
            Log.Error("Product with Id:{productId} doesn't exist", request.Id);
            throw new Exception("Product with Id: " + request.Id + " doesn't exist");
        }

        if (await _productRepository.IsProductOrderedBeforeAsync(request.Id))
        {
            Log.Error("Product with Id: {productId} cannot be deleted because it is associated with past orders.", request.Id);
            throw new Exception("Product with Id: " + request.Id + " cannot be deleted because it is associated with past orders.");
        }

        await _productRepository.DeleteAsync(request.Id);
        return Unit.Value;
    }
}