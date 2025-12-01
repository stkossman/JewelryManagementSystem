using Application.Common;
using Domain.Entities.JewelryCareSchedules;
using MediatR;

namespace Application.CareSchedules.Queries;

public record GetCareScheduleByIdQuery(Guid Id) : IRequest<Result<JewelryCareSchedule>>;