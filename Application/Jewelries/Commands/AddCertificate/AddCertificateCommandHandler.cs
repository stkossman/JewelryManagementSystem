using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Jewelries.Commands.AddCertificate;

public class AddCertificateCommandHandler : IRequestHandler<AddCertificateCommand, Result<Guid>>
{
    private readonly IJewelryCertificateRepository _certificateRepository;
    private readonly IJewelryRepository _jewelryRepository;

    public AddCertificateCommandHandler(
        IJewelryCertificateRepository certificateRepository,
        IJewelryRepository jewelryRepository)
    {
        _certificateRepository = certificateRepository;
        _jewelryRepository = jewelryRepository;
    }

    public async Task<Result<Guid>> Handle(AddCertificateCommand request, CancellationToken cancellationToken)
    {
        var jewelryId = new JewelryId(request.JewelryId);

        var jewelryExists = await _jewelryRepository.ExistsAsync(jewelryId, cancellationToken);
        if (!jewelryExists)
            return Result<Guid>.Failure("Jewelry not found");

        var existingCert = await _certificateRepository.GetByJewelryIdAsync(jewelryId, cancellationToken);
        if (existingCert != null)
            return Result<Guid>.Failure("Jewelry already has a certificate");

        var certificate = JewelryCertificate.New(
            JewelryCertificateId.New(),
            jewelryId,
            request.CertificateNumber,
            request.IssuedBy
        );

        await _certificateRepository.AddAsync(certificate, cancellationToken);

        return Result<Guid>.Success(certificate.Id.Value);
    }
}