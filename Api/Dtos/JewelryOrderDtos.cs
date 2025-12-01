using Domain.Enums;

namespace Api.Dtos;

public record CreateJewelryOrderRequest(
    string OrderNumber,
    Guid JewelryId,
    string CustomerName,
    string? Notes,
    OrderPriority Priority,
    DateTime ScheduledDate
);

public record UpdateJewelryOrderRequest(
    string CustomerName,
    string? Notes,
    OrderPriority Priority,
    DateTime ScheduledDate
);

public record CompleteJewelryOrderRequest(
    string? CompletionNotes
);

public record JewelryOrderResponse(
    Guid Id,
    string OrderNumber,
    Guid JewelryId,
    string CustomerName,
    string? Notes,
    OrderPriority Priority,
    OrderStatus Status,
    DateTime ScheduledDate,
    DateTime? CompletedAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);