using Application.Common;
using Application.Common.Interfaces.Queries;
using Domain.Entities.JewelryCareSchedules;
using MediatR;

namespace Application.CareSchedules.Queries;

public class GetAllCareSchedulesQueryHandler : IRequestHandler<GetAllCareSchedulesQuery, Result<IEnumerable<JewelryCareSchedule>>>
{
    private readonly IJewelryCareScheduleQueries _queries;

    public GetAllCareSchedulesQueryHandler(IJewelryCareScheduleQueries queries)
    {
        _queries = queries;
    }

    public async Task<Result<IEnumerable<JewelryCareSchedule>>> Handle(GetAllCareSchedulesQuery request, CancellationToken cancellationToken)
    {
        var schedules = await _queries.GetAllAsync(cancellationToken);

        return Result<IEnumerable<JewelryCareSchedule>>.Success(schedules);
    }
}