using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Collections.Commands.AddJewelryToCollection;

public class AddJewelryToCollectionCommandHandler : IRequestHandler<AddJewelryToCollectionCommand, Result>
{
    private readonly ICollectionRepository _collectionRepository;
    private readonly IJewelryRepository _jewelryRepository;

    public AddJewelryToCollectionCommandHandler(
        ICollectionRepository collectionRepository,
        IJewelryRepository jewelryRepository)
    {
        _collectionRepository = collectionRepository;
        _jewelryRepository = jewelryRepository;
    }

    public async Task<Result> Handle(AddJewelryToCollectionCommand request, CancellationToken cancellationToken)
    {
        var collectionId = new CollectionId(request.CollectionId);
        var jewelryId = new JewelryId(request.JewelryId);
        
        var collection = await _collectionRepository.GetByIdAsync(collectionId, cancellationToken);
        if (collection is null)
            return Result.Failure("Collection not found");

        var jewelry = await _jewelryRepository.GetByIdAsync(jewelryId, cancellationToken);
        if (jewelry is null)
            return Result.Failure("Jewelry not found");

        collection.Jewelries.Add(jewelry);

        await _collectionRepository.UpdateAsync(collection, cancellationToken);

        return Result.Success();
    }
}