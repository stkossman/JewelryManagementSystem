using Application.Common;
using Domain.Enums;
using MediatR;

namespace Application.Orders.Commands;

public record CreateJewelryOrderCommand(
    string OrderNumber,
    Guid JewelryId,
    string CustomerName,
    string? Notes,
    OrderPriority Priority,
    DateTime ScheduledDate
) : IRequest<Result<Guid>>;