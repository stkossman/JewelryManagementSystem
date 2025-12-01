using Application.Common;
using MediatR;

namespace Application.Orders.Commands;

public record CancelJewelryOrderCommand(Guid Id) : IRequest<Result>;