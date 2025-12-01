using Domain.Entities;

namespace Application.Common.Interfaces.Queries;

public interface ICollectionQueries
{
    Task<List<Collection>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Collection?> GetByIdWithJewelriesAsync(CollectionId id, CancellationToken cancellationToken = default);
}