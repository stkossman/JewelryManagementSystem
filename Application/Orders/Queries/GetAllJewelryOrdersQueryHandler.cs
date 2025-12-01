using Application.Common;
using Application.Common.Interfaces.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Orders.Queries;

public class GetAllJewelryOrdersQueryHandler : IRequestHandler<GetAllJewelryOrdersQuery, Result<IEnumerable<JewelryOrder>>>
{
    private readonly IJewelryOrderQueries _queries;

    public GetAllJewelryOrdersQueryHandler(IJewelryOrderQueries queries)
    {
        _queries = queries;
    }

    public async Task<Result<IEnumerable<JewelryOrder>>> Handle(GetAllJewelryOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _queries.GetAllAsync(cancellationToken);
        return Result<IEnumerable<JewelryOrder>>.Success(orders);
    }
}