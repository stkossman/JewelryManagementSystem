namespace Domain.Entities;

public record CollectionId(Guid Value)
{
    public static CollectionId Empty() => new(Guid.Empty);
    public static CollectionId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}

public class Collection
{
    public CollectionId Id { get; }
    public string Title { get; private set; }
    
    public List<Jewelry> Jewelries { get; private set; } = new();

    private Collection(CollectionId id, string title)
    {
        Id = id;
        Title = title;
    }

    public static Collection New(CollectionId id, string title)
        => new(id, title);

    public void UpdateTitle(string newTitle)
    {
        Title = newTitle;
    }
}