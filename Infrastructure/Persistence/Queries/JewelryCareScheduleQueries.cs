using Application.Common.Interfaces.Queries;
using Domain.Entities;
using Domain.Entities.JewelryCareSchedules;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Queries;

public class JewelryCareScheduleQueries : IJewelryCareScheduleQueries
{
    private readonly ApplicationDbContext _context;

    public JewelryCareScheduleQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<JewelryCareSchedule?> GetByIdAsync(JewelryCareScheduleId id, CancellationToken cancellationToken = default)
    {
        return await _context.JewelryCareSchedules
            .AsNoTracking()
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
}