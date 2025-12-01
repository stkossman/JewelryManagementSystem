using Application.Common;
using Domain.Enums;
using MediatR;

namespace Application.CareSchedules.Commands;

public record UpdateCareScheduleCommand(
    Guid Id,
    DateTime NextServiceDate,
    CareInterval Interval,
    string Description
) : IRequest<Result>;