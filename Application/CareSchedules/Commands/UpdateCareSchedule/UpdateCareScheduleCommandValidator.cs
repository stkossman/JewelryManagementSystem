using FluentValidation;

namespace Application.CareSchedules.Commands;

public class UpdateCareScheduleCommandValidator : AbstractValidator<UpdateCareScheduleCommand>
{
    public UpdateCareScheduleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.NextServiceDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
            .WithMessage("Next service date cannot be in the past");

        RuleFor(x => x.Interval)
            .IsInEnum().WithMessage("Invalid care interval");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
    }
}