using FluentValidation;

namespace Application.CareSchedules.Commands;

public class ReactivateCareScheduleCommandValidator : AbstractValidator<ReactivateCareScheduleCommand>
{
    public ReactivateCareScheduleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.NextServiceDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
            .WithMessage("Next service date cannot be in the past");
    }
}