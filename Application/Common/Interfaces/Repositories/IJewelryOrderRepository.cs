using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface IJewelryOrderRepository
{
    Task<JewelryOrder?> GetByIdAsync(JewelryOrderId id, CancellationToken cancellationToken = default);
    Task<IEnumerable<JewelryOrder>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<JewelryOrder>> GetByJewelryIdAsync(JewelryId jewelryId, CancellationToken cancellationToken = default);
    Task AddAsync(JewelryOrder order, CancellationToken cancellationToken = default);
    Task UpdateAsync(JewelryOrder order, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(JewelryOrderId id, CancellationToken cancellationToken = default);
}