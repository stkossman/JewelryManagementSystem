using FluentValidation;

namespace Application.Jewelry.Commands;

public class UpdateJewelryStatusCommandValidator : AbstractValidator<UpdateJewelryStatusCommand>
{
    public UpdateJewelryStatusCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status");
    }
}