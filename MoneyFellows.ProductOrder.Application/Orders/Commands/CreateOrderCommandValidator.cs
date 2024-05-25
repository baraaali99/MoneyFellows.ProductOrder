using FluentValidation;
using System;
using System.Collections.Generic;

namespace MoneyFellows.ProductOrder.Application.Orders.Commands
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.DeliveryAddress)
                .NotNull().WithMessage("Delivery Address is required.")
                .NotEmpty().WithMessage("Delivery Address is required.")
                .Matches("^[a-zA-Z0-9 ]*$").WithMessage("Delivery Address must be alphanumeric.");

            RuleFor(x => x.TotalCost)
                .NotNull().WithMessage("Total Cost is required.")
                .GreaterThan(0).WithMessage("Total Cost must be greater than zero.");

            RuleFor(x => x.OrderDetails)
                .NotNull().WithMessage("Order Details are required.")
                .NotEmpty().WithMessage("Order Details are required.")
                .Must(od => od.Count > 0).WithMessage("Order must contain at least one product.")
                .ForEach(od =>
                {
                    od.ChildRules(orderDetail =>
                    {
                        orderDetail.RuleFor(od => od.ProductId)
                            .NotEmpty().WithMessage("Product ID is required.");
                        orderDetail.RuleFor(od => od.Quantity)
                            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
                    });
                });

            RuleFor(x => x.CustomerDetails)
                .NotNull().WithMessage("Customer Details are required.")
                .NotEmpty().WithMessage("Customer Details are required.");

            RuleFor(x => x.DeliveryTime)
                .NotNull().WithMessage("Delivery Time is required.")
                .GreaterThan(DateTime.Now).WithMessage("Delivery Time must be in the future.");
        }
    }
}
