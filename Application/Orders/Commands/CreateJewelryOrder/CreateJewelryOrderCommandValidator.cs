using FluentValidation;

namespace Application.Orders.Commands;

public class CreateJewelryOrderCommandValidator : AbstractValidator<CreateJewelryOrderCommand>
{
    public CreateJewelryOrderCommandValidator()
    {
        RuleFor(x => x.OrderNumber)
            .NotEmpty().WithMessage("Order number is required")
            .MaximumLength(50).WithMessage("Order number must not exceed 50 characters");

        RuleFor(x => x.JewelryId)
            .NotEmpty().WithMessage("Jewelry ID is required");

        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("Customer name is required")
            .MaximumLength(200).WithMessage("Customer name must not exceed 200 characters");

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithMessage("Notes must not exceed 1000 characters");

        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Invalid priority");

        RuleFor(x => x.ScheduledDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Scheduled date cannot be in the past");
    }
}