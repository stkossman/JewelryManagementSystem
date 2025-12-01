using Application.Common;
using MediatR;

namespace Application.Orders.Commands;

public record StartJewelryOrderCommand(Guid Id) : IRequest<Result>;