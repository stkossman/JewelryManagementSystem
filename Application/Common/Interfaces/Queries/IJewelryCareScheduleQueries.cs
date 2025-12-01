using Domain.Entities;
using Domain.Entities.JewelryCareSchedules;

namespace Application.Common.Interfaces.Queries;

public interface IJewelryCareScheduleQueries
{
    Task<JewelryCareSchedule?> GetByIdAsync(JewelryCareScheduleId id, CancellationToken cancellationToken = default);
    Task<IEnumerable<JewelryCareSchedule>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<JewelryCareSchedule>> GetByJewelryIdAsync(JewelryId jewelryId, CancellationToken cancellationToken = default);
}