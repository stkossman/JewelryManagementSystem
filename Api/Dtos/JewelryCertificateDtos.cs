namespace Api.Dtos;

public record JewelryCertificateResponse(
    Guid Id,
    string CertificateNumber,
    string IssuedBy,
    Guid JewelryId
);

public record CreateJewelryCertificateRequest(
    Guid JewelryId,
    string CertificateNumber,
    string IssuedBy
);