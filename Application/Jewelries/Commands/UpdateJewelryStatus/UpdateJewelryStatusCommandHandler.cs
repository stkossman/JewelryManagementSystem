using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Jewelry.Commands;

public class UpdateJewelryStatusCommandHandler : IRequestHandler<UpdateJewelryStatusCommand, Result>
{
    private readonly IJewelryRepository _repository;

    public UpdateJewelryStatusCommandHandler(IJewelryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(UpdateJewelryStatusCommand request, CancellationToken cancellationToken)
    {
        var jewelryId = new JewelryId(request.Id);
        var jewelry = await _repository.GetByIdAsync(jewelryId, cancellationToken);

        if (jewelry is null)
            return Result.Failure("Jewelry not found");

        jewelry.ChangeStatus(request.Status);

        await _repository.UpdateAsync(jewelry, cancellationToken);

        return Result.Success();
    }
}