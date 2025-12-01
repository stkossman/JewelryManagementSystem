using Domain.Enums;

namespace Domain.Entities.JewelryCareSchedules;

public record JewelryCareScheduleId(Guid Value)
{
    public static JewelryCareScheduleId Empty() => new(Guid.Empty);
    public static JewelryCareScheduleId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

public class JewelryCareSchedule
{
    public JewelryCareScheduleId Id { get; }
    public JewelryId JewelryId { get; private set; }
    public DateTime NextServiceDate { get; private set; }
    public CareInterval Interval { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private JewelryCareSchedule(
        JewelryCareScheduleId id, JewelryId jewelryId, DateTime nextServiceDate,
        CareInterval interval, string description, DateTime createdAt)
    {
        Id = id;
        JewelryId = jewelryId;
        NextServiceDate = nextServiceDate;
        Interval = interval;
        Description = description;
        IsActive = true;
        CreatedAt = createdAt;
        UpdatedAt = null;
    }

    public static JewelryCareSchedule New(
        JewelryCareScheduleId id, JewelryId jewelryId, DateTime nextServiceDate,
        CareInterval interval, string description, DateTime createdAt)
        => new JewelryCareSchedule(id, jewelryId, nextServiceDate, interval, description, createdAt);

    public void UpdateSchedule(DateTime nextServiceDate, CareInterval interval, string description)
    {
        NextServiceDate = nextServiceDate;
        Interval = interval;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reactivate(DateTime nextServiceDate)
    {
        IsActive = true;
        NextServiceDate = nextServiceDate;
        UpdatedAt = DateTime.UtcNow;
    }
}
