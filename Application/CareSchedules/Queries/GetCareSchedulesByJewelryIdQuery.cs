using Application.Common;
using Domain.Entities.JewelryCareSchedules;
using MediatR;

namespace Application.CareSchedules.Queries;

public record GetCareSchedulesByJewelryIdQuery(Guid JewelryId) : IRequest<Result<IEnumerable<JewelryCareSchedule>>>;