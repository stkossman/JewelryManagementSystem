using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Jewelry.Commands;

public class UpdateJewelryCommandHandler : IRequestHandler<UpdateJewelryCommand, Result>
{
    private readonly IJewelryRepository _repository;

    public UpdateJewelryCommandHandler(IJewelryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(UpdateJewelryCommand request, CancellationToken cancellationToken)
    {
        var jewelryId = new JewelryId(request.Id);
        var jewelry = await _repository.GetByIdAsync(jewelryId, cancellationToken);

        if (jewelry is null)
            return Result.Failure("Jewelry not found");

        jewelry.UpdateDetails(
            request.Name,
            request.Description,
            request.JewelryType,
            request.Material,
            request.Price
        );

        await _repository.UpdateAsync(jewelry, cancellationToken);

        return Result.Success();
    }
}