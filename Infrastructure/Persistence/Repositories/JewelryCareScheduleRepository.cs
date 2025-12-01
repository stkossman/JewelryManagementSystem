using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Domain.Entities.JewelryCareSchedules;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class JewelryCareScheduleRepository : IJewelryCareScheduleRepository
{
    private readonly ApplicationDbContext _context;

    public JewelryCareScheduleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context; 
    
    public async Task<JewelryCareSchedule?> GetByIdAsync(JewelryCareScheduleId id, CancellationToken cancellationToken = default)
    {
        return await _context.JewelryCareSchedules
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<JewelryCareSchedule>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.JewelryCareSchedules
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<JewelryCareSchedule>> GetByJewelryIdAsync(JewelryId jewelryId, CancellationToken cancellationToken = default)
    {
        return await _context.JewelryCareSchedules
            .AsNoTracking()
            .Where(s => s.JewelryId == jewelryId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<JewelryCareSchedule>> GetUpcomingSchedulesAsync(DateTime fromDate, CancellationToken cancellationToken = default)
    {
        return await _context.JewelryCareSchedules
            .AsNoTracking()
            .Where(s => s.IsActive && s.NextServiceDate >= fromDate)
            .OrderBy(s => s.NextServiceDate)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(JewelryCareSchedule schedule, CancellationToken cancellationToken = default)
    {
        await _context.JewelryCareSchedules.AddAsync(schedule, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(JewelryCareSchedule schedule, CancellationToken cancellationToken = default)
    {
        _context.JewelryCareSchedules.Update(schedule);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(JewelryCareScheduleId id, CancellationToken cancellationToken = default)
    {
        return await _context.JewelryCareSchedules
            .AnyAsync(s => s.Id == id, cancellationToken);
    }
}
