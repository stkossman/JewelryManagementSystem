using Application.Common;
using MediatR;

namespace Application.CareSchedules.Commands;

public record ReactivateCareScheduleCommand(
    Guid Id,
    DateTime NextServiceDate
) : IRequest<Result>;