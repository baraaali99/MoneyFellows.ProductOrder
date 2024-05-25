using FluentValidation;

namespace MoneyFellows.ProductOrder.Application.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Product Id is required.")
                .GreaterThan(0).WithMessage("Product Id must be greater than zero.");
        }
    }
}
