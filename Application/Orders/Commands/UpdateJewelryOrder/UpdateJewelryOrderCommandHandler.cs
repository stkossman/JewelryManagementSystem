using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Orders.Commands;

public class UpdateJewelryOrderCommandHandler : IRequestHandler<UpdateJewelryOrderCommand, Result>
{
    private readonly IJewelryOrderRepository _repository;

    public UpdateJewelryOrderCommandHandler(IJewelryOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(UpdateJewelryOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = new JewelryOrderId(request.Id);

        var order = await _repository.GetByIdAsync(orderId, cancellationToken);

        if (order is null)
            return Result.Failure("Order not found");

        order.UpdateDetails(
            request.CustomerName,
            request.Notes,
            request.Priority,
            request.ScheduledDate
        );

        await _repository.UpdateAsync(order, cancellationToken);

        return Result.Success();
    }
}