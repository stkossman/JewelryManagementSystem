using Application.Common;
using Application.Common.Interfaces.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Orders.Queries;

public class GetJewelryOrderByIdQueryHandler : IRequestHandler<GetJewelryOrderByIdQuery, Result<JewelryOrder>>
{
    private readonly IJewelryOrderQueries _queries;

    public GetJewelryOrderByIdQueryHandler(IJewelryOrderQueries queries)
    {
        _queries = queries;
    }

    public async Task<Result<JewelryOrder>> Handle(GetJewelryOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var orderId = new JewelryOrderId(request.Id);
        var order = await _queries.GetByIdAsync(orderId, cancellationToken);

        return order is null
            ? Result<JewelryOrder>.Failure("Order not found")
            : Result<JewelryOrder>.Success(order);
    }
}