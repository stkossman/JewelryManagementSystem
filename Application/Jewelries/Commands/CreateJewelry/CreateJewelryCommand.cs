using Application.Common;
using Domain.Enums;
using MediatR;

namespace Application.Jewelry.Commands;

public record CreateJewelryCommand(
    string Name,
    string Description,
    JewelryType JewelryType,
    Material Material,
    decimal Price
) : IRequest<Result<Guid>>;