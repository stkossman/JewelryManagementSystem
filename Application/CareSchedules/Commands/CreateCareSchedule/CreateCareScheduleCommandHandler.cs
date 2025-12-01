using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Domain.Entities.JewelryCareSchedules;
using MediatR;

namespace Application.CareSchedules.Commands;

public class CreateCareScheduleCommandHandler : IRequestHandler<CreateCareScheduleCommand, Result<Guid>>
{
    private readonly IJewelryCareScheduleRepository _scheduleRepository;
    private readonly IJewelryRepository _jewelryRepository;

    public CreateCareScheduleCommandHandler(
        IJewelryCareScheduleRepository scheduleRepository,
        IJewelryRepository jewelryRepository)
    {
        _scheduleRepository = scheduleRepository;
        _jewelryRepository = jewelryRepository;
    }

    public async Task<Result<Guid>> Handle(CreateCareScheduleCommand request, CancellationToken cancellationToken)
    {
        var jewelryId = new JewelryId(request.JewelryId);

        var jewelryExists = await _jewelryRepository.ExistsAsync(jewelryId, cancellationToken);
        if (!jewelryExists)
            return Result<Guid>.Failure("Jewelry not found");

        var schedule = JewelryCareSchedule.New(
            JewelryCareScheduleId.New(),
            jewelryId,
            request.NextServiceDate,
            request.Interval,
            request.Description,
            DateTime.UtcNow
        );

        await _scheduleRepository.AddAsync(schedule, cancellationToken);

        return Result<Guid>.Success(schedule.Id.Value);
    }
}