using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface IJewelryCertificateRepository
{
    Task<JewelryCertificate?> GetByIdAsync(JewelryCertificateId id, CancellationToken cancellationToken = default);
    Task<JewelryCertificate?> GetByJewelryIdAsync(JewelryId jewelryId, CancellationToken cancellationToken = default);
    Task AddAsync(JewelryCertificate certificate, CancellationToken cancellationToken = default);
}