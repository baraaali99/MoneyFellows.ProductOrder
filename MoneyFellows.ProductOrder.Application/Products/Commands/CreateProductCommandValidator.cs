using FluentValidation;

namespace MoneyFellows.ProductOrder.Application.Products.Commands;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.ProductName)
            .NotNull().WithMessage("Product Name is required.")
            .NotEmpty().WithMessage("Product Name is required.")
            .Matches("^[a-zA-Z0-9]*$").WithMessage("Product Name must be alphanumeric.");
        RuleFor(x => x.ProductDescription)
            .NotNull().WithMessage("Product Description is required.")
            .NotEmpty().WithMessage("Product Description is required.")
            .Matches("^[a-zA-Z0-9]*$").WithMessage("Product Description must be alphanumeric.");
        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Product price is required.")
            .GreaterThan(0).WithMessage("Product price must be greater than zero.");
        RuleFor(x => x.Merchant)
            .NotNull().WithMessage("Merchant is required.")
            .NotEmpty().WithMessage("Merchant is required.")
            .Matches("^[a-zA-Z0-9]*$").WithMessage("Merchant must be alphanumeric.");
    }
}