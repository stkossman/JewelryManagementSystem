using Application.Common;
using Application.Common.Interfaces.Queries;
using Domain.Entities;
using Domain.Entities.JewelryCareSchedules;
using MediatR;

namespace Application.CareSchedules.Queries;

public class GetCareSchedulesByJewelryIdQueryHandler : IRequestHandler<GetCareSchedulesByJewelryIdQuery, Result<IEnumerable<JewelryCareSchedule>>>
{
    private readonly IJewelryCareScheduleQueries _queries;

    public GetCareSchedulesByJewelryIdQueryHandler(IJewelryCareScheduleQueries queries)
    {
        _queries = queries;
    }

    public async Task<Result<IEnumerable<JewelryCareSchedule>>> Handle(GetCareSchedulesByJewelryIdQuery request, CancellationToken cancellationToken)
    {
        var jewelryId = new JewelryId(request.JewelryId);
        var schedules = await _queries.GetByJewelryIdAsync(jewelryId, cancellationToken);
        
        return Result<IEnumerable<JewelryCareSchedule>>.Success(schedules);
    }
}