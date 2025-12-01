using Application.Common.Interfaces.Queries;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Queries;

public class JewelryCertificateQueries : IJewelryCertificateQueries
{
    private readonly ApplicationDbContext _context;

    public JewelryCertificateQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<JewelryCertificate?> GetByJewelryIdAsync(JewelryId jewelryId, CancellationToken cancellationToken = default)
    {
        return await _context.JewelryCertificates
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.JewelryId == jewelryId, cancellationToken);
    }
}