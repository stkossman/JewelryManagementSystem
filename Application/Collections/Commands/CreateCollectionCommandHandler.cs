using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Collections.Commands.CreateCollection;

public class CreateCollectionCommandHandler : IRequestHandler<CreateCollectionCommand, Result<Guid>>
{
    private readonly ICollectionRepository _repository;

    public CreateCollectionCommandHandler(ICollectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
    {
        var collection = Collection.New(
            CollectionId.New(),
            request.Title
        );

        await _repository.AddAsync(collection, cancellationToken);

        return Result<Guid>.Success(collection.Id.Value);
    }
}