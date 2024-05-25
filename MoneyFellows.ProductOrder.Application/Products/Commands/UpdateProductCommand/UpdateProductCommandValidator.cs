using FluentValidation;

namespace MoneyFellows.ProductOrder.Application.Products.Commands.UpdateProductCommand;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Product Id is required.")
            .GreaterThan(0).WithMessage("Product Id must be greater than zero.");
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