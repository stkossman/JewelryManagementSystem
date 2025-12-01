using Application.Common;
using MediatR;

namespace Application.Jewelry.Queries;

public record GetAllJewelryQuery : IRequest<Result<IEnumerable<Domain.Entities.Jewelry>>>;