using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class JewelryCertificateRepository : IJewelryCertificateRepository
{
    private readonly ApplicationDbContext _context;

    public JewelryCertificateRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<JewelryCertificate?> GetByIdAsync(JewelryCertificateId id, CancellationToken cancellationToken = default)
    {
        return await _context.JewelryCertificates
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<JewelryCertificate?> GetByJewelryIdAsync(JewelryId jewelryId, CancellationToken cancellationToken = default)
    {
        return await _context.JewelryCertificates
            .FirstOrDefaultAsync(c => c.JewelryId == jewelryId, cancellationToken);
    }

    public async Task AddAsync(JewelryCertificate certificate, CancellationToken cancellationToken = default)
    {
        await _context.JewelryCertificates.AddAsync(certificate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}