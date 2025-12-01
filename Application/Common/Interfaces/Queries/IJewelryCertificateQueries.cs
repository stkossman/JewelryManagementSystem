using Domain.Entities;

namespace Application.Common.Interfaces.Queries;

public interface IJewelryCertificateQueries
{
    Task<JewelryCertificate?> GetByJewelryIdAsync(JewelryId jewelryId, CancellationToken cancellationToken = default);
}