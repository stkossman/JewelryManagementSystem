using Application.Common.Interfaces.Queries;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Queries;

public class CollectionQueries : ICollectionQueries
{
    private readonly ApplicationDbContext _context;

    public CollectionQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Collection>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Collections
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Collection?> GetByIdWithJewelriesAsync(CollectionId id, CancellationToken cancellationToken = default)
    {
        return await _context.Collections
            .AsNoTracking()
            .Include(c => c.Jewelries) // Включаємо пов'язані вироби
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}