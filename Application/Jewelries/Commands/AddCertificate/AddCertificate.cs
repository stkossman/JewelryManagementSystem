using Application.Common;
using MediatR;

namespace Application.Jewelries.Commands.AddCertificate;

public record AddCertificateCommand(
    Guid JewelryId,
    string CertificateNumber,
    string IssuedBy
) : IRequest<Result<Guid>>;