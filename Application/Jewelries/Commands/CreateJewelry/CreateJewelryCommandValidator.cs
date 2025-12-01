using FluentValidation;

namespace Application.Jewelry.Commands;

public class CreateJewelryCommandValidator : AbstractValidator<CreateJewelryCommand>
{
    public CreateJewelryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");

        RuleFor(x => x.JewelryType)
            .IsInEnum().WithMessage("Invalid jewelry type");

        RuleFor(x => x.Material)
            .IsInEnum().WithMessage("Invalid material type");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}