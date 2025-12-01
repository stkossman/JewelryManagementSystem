using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Orders.Commands;

public class StartJewelryOrderCommandHandler : IRequestHandler<StartJewelryOrderCommand, Result>
{
    private readonly IJewelryOrderRepository _repository;

    public StartJewelryOrderCommandHandler(IJewelryOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(StartJewelryOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = new JewelryOrderId(request.Id);
        var order = await _repository.GetByIdAsync(orderId, cancellationToken);

        if (order is null)
            return Result.Failure("Order not found");

        order.StartWork();

        await _repository.UpdateAsync(order, cancellationToken);

        return Result.Success();
    }
}