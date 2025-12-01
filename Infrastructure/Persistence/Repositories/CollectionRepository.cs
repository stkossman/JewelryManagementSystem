using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class CollectionRepository : ICollectionRepository
{
    private readonly ApplicationDbContext _context;

    public CollectionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Collection?> GetByIdAsync(CollectionId id, CancellationToken cancellationToken = default)
    {
        return await _context.Collections
            .Include(c => c.Jewelries)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<List<Collection>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Collections
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Collection collection, CancellationToken cancellationToken = default)
    {
        await _context.Collections.AddAsync(collection, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Collection collection, CancellationToken cancellationToken = default)
    {
        _context.Collections.Update(collection);
        await _context.SaveChangesAsync(cancellationToken);
    }
}