using Domain.Entities;
using Domain.Enums;

namespace Tests.Data.Jewelry;

public static class JewelryData
{
    public static Domain.Entities.Jewelry CreateTestJewelry()
    {
        return Domain.Entities.Jewelry.New(
            JewelryId.New(),
            "Gold Ring 'Amulet'",
            "A beautiful gold ring with ancient symbols.",
            JewelryType.Ring,
            Material.Gold,
            350.50m,
            DateTime.UtcNow
        );
    }
}