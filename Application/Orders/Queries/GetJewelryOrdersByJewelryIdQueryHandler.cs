using Application.Common;
using Application.Common.Interfaces.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Orders.Queries;

public class GetJewelryOrdersByJewelryIdQueryHandler : IRequestHandler<GetJewelryOrdersByJewelryIdQuery, Result<IEnumerable<JewelryOrder>>>
{
    private readonly IJewelryOrderQueries _queries;

    public GetJewelryOrdersByJewelryIdQueryHandler(IJewelryOrderQueries queries)
    {
        _queries = queries;
    }

    public async Task<Result<IEnumerable<JewelryOrder>>> Handle(GetJewelryOrdersByJewelryIdQuery request, CancellationToken cancellationToken)
    {
        var jewelryId = new JewelryId(request.JewelryId);
        var orders = await _queries.GetByJewelryIdAsync(jewelryId, cancellationToken);

        return Result<IEnumerable<JewelryOrder>>.Success(orders);
    }
}