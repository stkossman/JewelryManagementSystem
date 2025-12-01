using Domain.Enums;

namespace Domain.Entities;

public record JewelryOrderId(Guid Value)
{
    public static JewelryOrderId Empty() => new(Guid.Empty);
    public static JewelryOrderId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

public class JewelryOrder
{
    public JewelryOrderId Id { get; }
    public string OrderNumber { get; private set; }
    public JewelryId JewelryId { get; private set; }
    public string CustomerName { get; private set; }
    public string? Notes { get; private set; }
    public OrderPriority Priority { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime ScheduledDate { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private JewelryOrder(
        JewelryOrderId id, string orderNumber, JewelryId jewelryId, string customerName,
        string? notes, OrderPriority priority, DateTime scheduledDate,
        DateTime createdAt)
    {
        Id = id;
        OrderNumber = orderNumber;
        JewelryId = jewelryId;
        CustomerName = customerName;
        Notes = notes;
        Priority = priority;
        Status = OrderStatus.Pending;
        ScheduledDate = scheduledDate;
        CreatedAt = createdAt;
        UpdatedAt = null;
    }

    public static JewelryOrder New(
        JewelryOrderId id, string orderNumber, JewelryId jewelryId, string customerName,
        string? notes, OrderPriority priority, DateTime scheduledDate, DateTime createdAt)
        => new JewelryOrder(id, orderNumber, jewelryId, customerName, notes, priority, scheduledDate, createdAt);

    public void UpdateDetails(
        string customerName, string? notes, OrderPriority priority, DateTime scheduledDate)
    {
        CustomerName = customerName;
        Notes = notes;
        Priority = priority;
        ScheduledDate = scheduledDate;
        UpdatedAt = DateTime.UtcNow;
    }

    public void StartWork()
    {
        Status = OrderStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete(string? completionNotes = null)
    {
        Status = OrderStatus.Completed;
        Notes = completionNotes;
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        Status = OrderStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }
}
