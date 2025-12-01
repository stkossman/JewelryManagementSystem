using Application.Common;
using Domain.Entities.JewelryCareSchedules;
using MediatR;

namespace Application.CareSchedules.Queries;

public record GetAllCareSchedulesQuery : IRequest<Result<IEnumerable<JewelryCareSchedule>>>;