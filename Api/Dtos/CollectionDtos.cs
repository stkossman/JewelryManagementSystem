namespace Api.Dtos;

public record CollectionResponse(
    Guid Id,
    string Title,
    List<Guid> JewelryIds
);

public record CreateCollectionRequest(string Title);

public record AddJewelryToCollectionRequest(Guid JewelryId);