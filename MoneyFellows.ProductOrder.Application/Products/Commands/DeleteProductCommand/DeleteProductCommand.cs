using MediatR;
using Microsoft.Extensions.Logging;
using MoneyFellows.ProductOrder.Core.Interfaces;

namespace MoneyFellows.ProductOrder.Application.Products.Commands.DeleteProductCommand;

public class DeleteProductCommand : IRequest
{
    public int Id { get; set; }

}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<DeleteProductCommand> _logger;
    public DeleteProductCommandHandler(IProductRepository productRepository
        , ILogger<DeleteProductCommand> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            _logger.LogError("Product with Id:{productId} doesn't exist", request.Id);
            throw new Exception("Product with Id: " + request.Id + " doesn't exist");
        }

        if (await _productRepository.IsProductOrderedBeforeAsync(request.Id))
        {
            _logger.LogError("Product with Id: {productId} cannot be deleted because it is associated with past orders.", request.Id);
            throw new Exception("Product with Id: " + request.Id + " cannot be deleted because it is associated with past orders.");
        }

        await _productRepository.DeleteAsync(request.Id);
        return Unit.Value;
    }
}