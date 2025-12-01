using Application.Common;
using Domain.Enums;
using MediatR;

namespace Application.Jewelry.Commands;

public record UpdateJewelryStatusCommand(
    Guid Id,
    JewelryStatus Status
) : IRequest<Result>;