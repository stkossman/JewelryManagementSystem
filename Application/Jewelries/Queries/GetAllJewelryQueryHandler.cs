using Application.Common;
using Application.Common.Interfaces.Queries;
using MediatR;

namespace Application.Jewelry.Queries;

public class GetAllJewelryQueryHandler : IRequestHandler<GetAllJewelryQuery, Result<IEnumerable<Domain.Entities.Jewelry>>>
{
    private readonly IJewelryQueries _queries;

    public GetAllJewelryQueryHandler(IJewelryQueries queries)
    {
        _queries = queries;
    }

    public async Task<Result<IEnumerable<Domain.Entities.Jewelry>>> Handle(GetAllJewelryQuery request, CancellationToken cancellationToken)
    {
        var jewelry = await _queries.GetAllAsync(cancellationToken);

        return Result<IEnumerable<Domain.Entities.Jewelry>>.Success(jewelry);
    }
}