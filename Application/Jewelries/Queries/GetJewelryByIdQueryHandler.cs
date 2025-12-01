using Application.Common;
using Application.Common.Interfaces.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Jewelry.Queries;

public class GetJewelryByIdQueryHandler : IRequestHandler<GetJewelryByIdQuery, Result<Domain.Entities.Jewelry>>
{
    private readonly IJewelryQueries _queries;

    public GetJewelryByIdQueryHandler(IJewelryQueries queries)
    {
        _queries = queries;
    }

    public async Task<Result<Domain.Entities.Jewelry>> Handle(GetJewelryByIdQuery request, CancellationToken cancellationToken)
    {
        var jewelryId = new JewelryId(request.Id);
        var jewelry = await _queries.GetByIdAsync(jewelryId, cancellationToken);

        return jewelry is null
            ? Result<Domain.Entities.Jewelry>.Failure("Jewelry not found")
            : Result<Domain.Entities.Jewelry>.Success(jewelry);
    }
}