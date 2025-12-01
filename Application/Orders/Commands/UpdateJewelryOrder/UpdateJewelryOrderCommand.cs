using Application.Common;
using Domain.Enums;
using MediatR;

namespace Application.Orders.Commands;

public record UpdateJewelryOrderCommand(
    Guid Id,
    string CustomerName,
    string? Notes,
    OrderPriority Priority,
    DateTime ScheduledDate
) : IRequest<Result>;