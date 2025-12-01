using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Jewelry.Commands;

public class CreateJewelryCommandHandler : IRequestHandler<CreateJewelryCommand, Result<Guid>>
{
    private readonly IJewelryRepository _repository;

    public CreateJewelryCommandHandler(IJewelryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(CreateJewelryCommand request, CancellationToken cancellationToken)
    {
        var jewelry = Domain.Entities.Jewelry.New(
            JewelryId.New(),
            request.Name,
            request.Description,
            request.JewelryType,
            request.Material,
            request.Price,
            DateTime.UtcNow
        );

        await _repository.AddAsync(jewelry, cancellationToken);

        return Result<Guid>.Success(jewelry.Id.Value);
    }
}