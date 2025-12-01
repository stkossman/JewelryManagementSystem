using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Orders.Commands;

public class CancelJewelryOrderCommandHandler : IRequestHandler<CancelJewelryOrderCommand, Result>
{
    private readonly IJewelryOrderRepository _repository;

    public CancelJewelryOrderCommandHandler(IJewelryOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(CancelJewelryOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = new JewelryOrderId(request.Id);
        var order = await _repository.GetByIdAsync(orderId, cancellationToken);

        if (order is null)
            return Result.Failure("Order not found");

        order.Cancel();

        await _repository.UpdateAsync(order, cancellationToken);

        return Result.Success();
    }
}