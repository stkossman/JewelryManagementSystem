using Domain.Entities;

namespace Application.Common.Interfaces.Queries;

public interface IJewelryOrderQueries
{
    Task<JewelryOrder?> GetByIdAsync(JewelryOrderId id, CancellationToken cancellationToken = default);
    Task<IEnumerable<JewelryOrder>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<JewelryOrder>> GetByJewelryIdAsync(JewelryId jewelryId, CancellationToken cancellationToken = default);
}