using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Entities.JewelryCareSchedules;
using MediatR;

namespace Application.CareSchedules.Commands;

public class UpdateCareScheduleCommandHandler : IRequestHandler<UpdateCareScheduleCommand, Result>
{
    private readonly IJewelryCareScheduleRepository _repository;

    public UpdateCareScheduleCommandHandler(IJewelryCareScheduleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(UpdateCareScheduleCommand request, CancellationToken cancellationToken)
    {
        var scheduleId = new JewelryCareScheduleId(request.Id);
        var schedule = await _repository.GetByIdAsync(scheduleId, cancellationToken);

        if (schedule is null)
            return Result.Failure("Care schedule not found");

        schedule.UpdateSchedule(
            request.NextServiceDate,
            request.Interval,
            request.Description
        );

        await _repository.UpdateAsync(schedule, cancellationToken);

        return Result.Success();
    }
}