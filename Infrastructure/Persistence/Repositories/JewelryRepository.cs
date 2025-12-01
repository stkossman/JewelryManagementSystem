using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class JewelryRepository : IJewelryRepository
{
    private readonly ApplicationDbContext _context;

    public JewelryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Jewelry?> GetByIdAsync(JewelryId id, CancellationToken cancellationToken = default)
    {
        return await _context.Jewelries
            .FirstOrDefaultAsync(j => j.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Jewelry>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Jewelries
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Jewelry jewelry, CancellationToken cancellationToken = default)
    {
        await _context.Jewelries.AddAsync(jewelry, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Jewelry jewelry, CancellationToken cancellationToken = default)
    {
        _context.Jewelries.Update(jewelry);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(JewelryId id, CancellationToken cancellationToken = default)
    {
        return await _context.Jewelries
            .AnyAsync(j => j.Id == id, cancellationToken);
    }
}