using Domain.Entities;
using Domain.Entities.JewelryCareSchedules;
using Domain.Enums;

namespace Tests.Data.JewelryCareSchedules;

public static class JewelryCareScheduleData
{
    public static JewelryCareSchedule CreateTestSchedule(JewelryId jewelryId)
    {
        return JewelryCareSchedule.New(
            JewelryCareScheduleId.New(),
            jewelryId,
            DateTime.UtcNow.AddMonths(6),
            CareInterval.Monthly,
            "Standard semi-annual cleaning and check.",
            DateTime.UtcNow
        );
    }
}