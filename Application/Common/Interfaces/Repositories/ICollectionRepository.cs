using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface ICollectionRepository
{
    Task<Collection?> GetByIdAsync(CollectionId id, CancellationToken cancellationToken = default);
    Task<List<Collection>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Collection collection, CancellationToken cancellationToken = default);
    Task UpdateAsync(Collection collection, CancellationToken cancellationToken = default);
}