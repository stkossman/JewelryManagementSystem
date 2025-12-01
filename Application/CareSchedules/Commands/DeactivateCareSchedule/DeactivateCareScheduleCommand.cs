using Application.Common;
using MediatR;

namespace Application.CareSchedules.Commands;

public record DeactivateCareScheduleCommand(Guid Id) : IRequest<Result>;