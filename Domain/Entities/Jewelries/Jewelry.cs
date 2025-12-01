using Domain.Enums;

namespace Domain.Entities;

public record JewelryId(Guid Value)
{
    public static JewelryId Empty() => new(Guid.Empty);
    public static JewelryId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

public class Jewelry
{
    public JewelryId Id { get; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public JewelryType JewelryType { get; private set; }
    public Material Material { get; private set; }
    public decimal Price { get; private set; }
    public JewelryStatus Status { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }
    
    public JewelryCertificate? Certificate { get; private set; }
    private readonly List<Collection> _collections = new();
    public IReadOnlyCollection<Collection> Collections => _collections.AsReadOnly();

    private Jewelry(
        JewelryId id, string name, string description, JewelryType jewelryType,
        Material material, decimal price, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Description = description;
        JewelryType = jewelryType;
        Material = material;
        Price = price;
        Status = JewelryStatus.Available;
        CreatedAt = createdAt;
        UpdatedAt = null;
    }

    public static Jewelry New(
        JewelryId id, string name, string description,
        JewelryType jewelryType, Material material, decimal price, DateTime createdAt)
        => new Jewelry(id, name, description, jewelryType, material, price, createdAt);

    public void UpdateDetails(
        string name, string description, JewelryType jewelryType,
        Material material, decimal price)
    {
        Name = name;
        Description = description;
        JewelryType = jewelryType;
        Material = material;
        Price = price;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeStatus(JewelryStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void AddToCollection(Collection collection)
    {
        if (!_collections.Any(c => c.Id == collection.Id))
        {
            _collections.Add(collection);
        }
    }

    public void RemoveFromCollection(CollectionId collectionId)
    {
        var collection = _collections.FirstOrDefault(c => c.Id == collectionId);
        if (collection != null)
        {
            _collections.Remove(collection);
        }
    }
}