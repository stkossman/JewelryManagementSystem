using Domain.Entities;

namespace Application.Common.Interfaces.Queries;

public interface IJewelryQueries
{
    Task<Domain.Entities.Jewelry?> GetByIdAsync(JewelryId id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Domain.Entities.Jewelry>> GetAllAsync(CancellationToken cancellationToken = default);
}