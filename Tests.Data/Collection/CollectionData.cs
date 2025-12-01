using Domain.Entities;

namespace Tests.Data.Collections;

public static class CollectionData
{
    public static Collection CreateTestCollection()
    {
        return Collection.New(
            CollectionId.New(), 
            "Winter Collection 2025"
        );
    }
}