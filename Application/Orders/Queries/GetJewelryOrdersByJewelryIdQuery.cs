using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.Orders.Queries;

public record GetJewelryOrdersByJewelryIdQuery(Guid JewelryId) : IRequest<Result<IEnumerable<JewelryOrder>>>;