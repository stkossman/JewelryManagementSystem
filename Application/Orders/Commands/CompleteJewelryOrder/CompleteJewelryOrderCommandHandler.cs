using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Orders.Commands;

public class CompleteJewelryOrderCommandHandler : IRequestHandler<CompleteJewelryOrderCommand, Result>
{
    private readonly IJewelryOrderRepository _repository;

    public CompleteJewelryOrderCommandHandler(IJewelryOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(CompleteJewelryOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = new JewelryOrderId(request.Id);
        var order = await _repository.GetByIdAsync(orderId, cancellationToken);

        if (order is null)
            return Result.Failure("Order not found");

        order.Complete(request.CompletionNotes);

        await _repository.UpdateAsync(order, cancellationToken);

        return Result.Success();
    }
}