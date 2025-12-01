using Application.Common;
using Application.Common.Interfaces.Queries;
using Domain.Entities.JewelryCareSchedules;
using MediatR;

namespace Application.CareSchedules.Queries;

public class GetCareScheduleByIdQueryHandler : IRequestHandler<GetCareScheduleByIdQuery, Result<JewelryCareSchedule>>
{
    private readonly IJewelryCareScheduleQueries _queries;

    public GetCareScheduleByIdQueryHandler(IJewelryCareScheduleQueries queries)
    {
        _queries = queries;
    }

    public async Task<Result<JewelryCareSchedule>> Handle(GetCareScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        var scheduleId = new JewelryCareScheduleId(request.Id);
        var schedule = await _queries.GetByIdAsync(scheduleId, cancellationToken);

        return schedule is null
            ? Result<JewelryCareSchedule>.Failure("Care schedule not found")
            : Result<JewelryCareSchedule>.Success(schedule);
    }
}