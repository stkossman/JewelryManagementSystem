using Application.Common.Interfaces.Queries;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Queries;

public class JewelryQueries : IJewelryQueries
{
    private readonly ApplicationDbContext _context;

    public JewelryQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Jewelry?> GetByIdAsync(JewelryId id, CancellationToken cancellationToken = default)
    {
        return await _context.Jewelries
            .AsNoTracking()
            .Include(j => j.Certificate)
            .Include(j => j.Collections)
            .FirstOrDefaultAsync(j => j.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Jewelry>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Jewelries
            .AsNoTracking()
            .Include(j => j.Certificate)
            .Include(j => j.Collections)
            .ToListAsync(cancellationToken);
    }
}