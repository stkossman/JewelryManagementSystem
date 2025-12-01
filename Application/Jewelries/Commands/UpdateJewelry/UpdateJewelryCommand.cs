using Application.Common;
using Domain.Enums;
using MediatR;

namespace Application.Jewelry.Commands;

public record UpdateJewelryCommand(
    Guid Id,
    string Name,
    string Description,
    JewelryType JewelryType,
    Material Material,
    decimal Price
) : IRequest<Result>;