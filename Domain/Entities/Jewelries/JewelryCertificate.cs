namespace Domain.Entities;

public record JewelryCertificateId(Guid Value)
{
    public static JewelryCertificateId Empty() => new(Guid.Empty);
    public static JewelryCertificateId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

public class JewelryCertificate
{
    public JewelryCertificateId Id { get; }
    public string CertificateNumber { get; private set; }
    public string IssuedBy { get; private set; }
    
    public JewelryId JewelryId { get; private set; }
    public Jewelry? Jewelry { get; private set; }
    
    private JewelryCertificate(
        JewelryCertificateId id, 
        JewelryId jewelryId, 
        string certificateNumber, 
        string issuedBy)
    {
        Id = id;
        JewelryId = jewelryId;
        CertificateNumber = certificateNumber;
        IssuedBy = issuedBy;
    }

    public static JewelryCertificate New(
        JewelryCertificateId id, 
        JewelryId jewelryId, 
        string certificateNumber, 
        string issuedBy)
        => new(id, jewelryId, certificateNumber, issuedBy);
}