using Domain.Enums;

namespace Api.Dtos;

public record CreateCareScheduleRequest(
    Guid JewelryId,
    DateTime NextServiceDate,
    CareInterval Interval,
    string Description
);

public record UpdateCareScheduleRequest(
    DateTime NextServiceDate,
    CareInterval Interval,
    string Description
);

public record ReactivateCareScheduleRequest(
    DateTime NextServiceDate
);

public record CareScheduleResponse(
    Guid Id,
    Guid JewelryId,
    DateTime NextServiceDate,
    CareInterval Interval,
    string Description,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);