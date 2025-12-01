using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.Orders.Queries;

public record GetAllJewelryOrdersQuery : IRequest<Result<IEnumerable<JewelryOrder>>>;