using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface IJewelryRepository
{
    Task<Domain.Entities.Jewelry?> GetByIdAsync(JewelryId id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Domain.Entities.Jewelry>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Domain.Entities.Jewelry jewelry, CancellationToken cancellationToken = default);
    Task UpdateAsync(Domain.Entities.Jewelry jewelry, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(JewelryId id, CancellationToken cancellationToken = default);
}