using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.Orders.Queries;

public record GetJewelryOrderByIdQuery(Guid Id) : IRequest<Result<JewelryOrder>>;