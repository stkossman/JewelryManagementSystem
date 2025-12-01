using Domain.Entities;
using Domain.Enums;

namespace Tests.Data.JewelryOrders;

public static class JewelryOrderData
{
    public static JewelryOrder CreateTestOrder(JewelryId jewelryId)
    {
        return JewelryOrder.New(
            JewelryOrderId.New(),
            $"ORD-{new Random().Next(1000, 9999)}",
            jewelryId,
            "John Doe",
            "Please deliver before 5 PM.",
            OrderPriority.High,
            DateTime.UtcNow.AddDays(14),
            DateTime.UtcNow
        );
    }
}