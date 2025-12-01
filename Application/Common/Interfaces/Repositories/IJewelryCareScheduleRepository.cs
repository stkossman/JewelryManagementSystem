using Domain.Entities;
using Domain.Entities.JewelryCareSchedules;

namespace Application.Common.Interfaces.Repositories;

public interface IJewelryCareScheduleRepository
{
    Task<JewelryCareSchedule?> GetByIdAsync(JewelryCareScheduleId id, CancellationToken cancellationToken = default);
    Task<IEnumerable<JewelryCareSchedule>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<JewelryCareSchedule>> GetByJewelryIdAsync(JewelryId jewelryId, CancellationToken cancellationToken = default);
    Task<IEnumerable<JewelryCareSchedule>> GetUpcomingSchedulesAsync(DateTime fromDate, CancellationToken cancellationToken = default);
    Task AddAsync(JewelryCareSchedule schedule, CancellationToken cancellationToken = default);
    Task UpdateAsync(JewelryCareSchedule schedule, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(JewelryCareScheduleId id, CancellationToken cancellationToken = default);
    IUnitOfWork UnitOfWork { get; }
}