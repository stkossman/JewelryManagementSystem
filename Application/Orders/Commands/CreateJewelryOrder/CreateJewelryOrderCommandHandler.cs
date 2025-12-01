using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Orders.Commands;

public class CreateJewelryOrderCommandHandler : IRequestHandler<CreateJewelryOrderCommand, Result<Guid>>
{
    private readonly IJewelryOrderRepository _orderRepository;
    private readonly IJewelryRepository _jewelryRepository;

    public CreateJewelryOrderCommandHandler(
        IJewelryOrderRepository orderRepository,
        IJewelryRepository jewelryRepository)
    {
        _orderRepository = orderRepository;
        _jewelryRepository = jewelryRepository;
    }

    public async Task<Result<Guid>> Handle(CreateJewelryOrderCommand request, CancellationToken cancellationToken)
    {
        var jewelryId = new JewelryId(request.JewelryId);

        var jewelryExists = await _jewelryRepository.ExistsAsync(jewelryId, cancellationToken);
        if (!jewelryExists)
            return Result<Guid>.Failure("Jewelry not found");

        var order = JewelryOrder.New(
            JewelryOrderId.New(),
            request.OrderNumber,
            jewelryId,
            request.CustomerName,
            request.Notes,
            request.Priority,
            request.ScheduledDate,
            DateTime.UtcNow
        );

        await _orderRepository.AddAsync(order, cancellationToken);

        return Result<Guid>.Success(order.Id.Value);
    }
}