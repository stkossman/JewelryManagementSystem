using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class JewelryOrderRepository : IJewelryOrderRepository
{
    private readonly ApplicationDbContext _context;

    public JewelryOrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<JewelryOrder?> GetByIdAsync(JewelryOrderId id, CancellationToken cancellationToken = default)
    {
        return await _context.JewelryOrders
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

    public async Task AddAsync(JewelryOrder order, CancellationToken cancellationToken = default)
    {
        await _context.JewelryOrders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(JewelryOrder order, CancellationToken cancellationToken = default)
    {
        _context.JewelryOrders.Update(order);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(JewelryOrderId id, CancellationToken cancellationToken = default)
    {
        return await _context.JewelryOrders
            .AnyAsync(o => o.Id == id, cancellationToken);
    }
}