using FluentValidation;

namespace MoneyFellows.ProductOrder.Application.Orders.Commands.DeleteOrderCommand
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Order ID is required.")
                .GreaterThan(0).WithMessage("Order ID must be greater than zero.");
        }
    }
}
