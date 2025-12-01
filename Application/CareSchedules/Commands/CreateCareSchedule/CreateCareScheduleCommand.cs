using Application.Common;
using Domain.Enums;
using MediatR;

namespace Application.CareSchedules.Commands;

public record CreateCareScheduleCommand(
    Guid JewelryId,
    DateTime NextServiceDate,
    CareInterval Interval,
    string Description
) : IRequest<Result<Guid>>;