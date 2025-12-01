using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Entities.JewelryCareSchedules;
using MediatR;

namespace Application.CareSchedules.Commands;

public class DeactivateCareScheduleCommandHandler : IRequestHandler<DeactivateCareScheduleCommand, Result>
{
    private readonly IJewelryCareScheduleRepository _repository;

    public DeactivateCareScheduleCommandHandler(IJewelryCareScheduleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(DeactivateCareScheduleCommand request, CancellationToken cancellationToken)
    {
        var scheduleId = new JewelryCareScheduleId(request.Id);
        var schedule = await _repository.GetByIdAsync(scheduleId, cancellationToken);

        if (schedule is null)
            return Result.Failure("Care schedule not found");

        schedule.Deactivate();

        await _repository.UpdateAsync(schedule, cancellationToken);

        return Result.Success();
    }
}