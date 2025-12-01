using Application.Common;
using MediatR;

namespace Application.Jewelry.Queries;

public record GetJewelryByIdQuery(Guid Id) : IRequest<Result<Domain.Entities.Jewelry>>;