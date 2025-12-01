using Domain.Entities;

namespace Tests.Data.Jewelry;

public static class JewelryCertificateData
{
    public static JewelryCertificate CreateTestCertificate(JewelryId jewelryId)
    {
        return JewelryCertificate.New(
            JewelryCertificateId.New(),
            jewelryId,
            "CERT-UA-" + new Random().Next(10000, 99999),
            "Gemological Institute of Ukraine"
        );
    }
}