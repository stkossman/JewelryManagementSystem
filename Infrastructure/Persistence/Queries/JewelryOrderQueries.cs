using Application.Common.Interfaces.Queries;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Queries;

public class JewelryOrderQueries : IJewelryOrderQueries
{
    private readonly ApplicationDbContext _context;

    public JewelryOrderQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<JewelryOrder?> GetByIdAsync(JewelryOrderId id, CancellationToken cancellationToken = default)
    {
        return await _context.JewelryOrders
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<JewelryOrder>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.JewelryOrders
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<JewelryOrder>> GetByJewelryIdAsync(JewelryId jewelryId, CancellationToken cancellationToken = default)
    {
        return await _context.JewelryOrders
            .AsNoTracking()
            .Where(o => o.JewelryId == jewelryId)
            .ToListAsync(cancellationToken);
    }
}