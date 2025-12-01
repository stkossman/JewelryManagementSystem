using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Entities.JewelryCareSchedules;
using MediatR;

namespace Application.CareSchedules.Commands;

public class ReactivateCareScheduleCommandHandler : IRequestHandler<ReactivateCareScheduleCommand, Result>
{
    private readonly IJewelryCareScheduleRepository _repository;

    public ReactivateCareScheduleCommandHandler(IJewelryCareScheduleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(ReactivateCareScheduleCommand request, CancellationToken cancellationToken)
    {
        var scheduleId = new JewelryCareScheduleId(request.Id);
        var schedule = await _repository.GetByIdAsync(scheduleId, cancellationToken);

        if (schedule is null)
            return Result.Failure("Care schedule not found");

        schedule.Reactivate(request.NextServiceDate);

        await _repository.UpdateAsync(schedule, cancellationToken);

        return Result.Success();
    }
}