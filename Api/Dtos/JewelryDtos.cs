using Domain.Enums;

namespace Api.Dtos;

public record CreateJewelryRequest(
    string Name,
    string Description,
    JewelryType JewelryType,
    Material Material,
    decimal Price
);

public record UpdateJewelryRequest(
    string Name,
    string Description,
    JewelryType JewelryType,
    Material Material,
    decimal Price
);

public record UpdateJewelryStatusRequest(
    JewelryStatus Status
);

public record JewelryResponse(
    Guid Id,
    string Name,
    string Description,
    JewelryType JewelryType,
    Material Material,
    decimal Price,
    JewelryStatus Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt,

    JewelryCertificateResponse? Certificate, 
    List<string> Collections
);