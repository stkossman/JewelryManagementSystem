using System.Net;
using System.Net.Http.Json;
using Api.Dtos;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using Tests.Data.Collections;
using Tests.Data.Jewelry;
using Xunit;

namespace Api.Tests.Integration.Collections;

public class CollectionsControllerTests(IntegrationTestWebFactory factory)
    : BaseIntegrationTest(factory), IAsyncLifetime
{
    private const string BaseRoute = "api/collections";
    private Domain.Entities.Jewelry _testJewelry = null!;
    private Collection _testCollection = null!;

    public async Task InitializeAsync()
    {
        _testJewelry = JewelryData.CreateTestJewelry();
        _testCollection = CollectionData.CreateTestCollection();

        await Context.Jewelries.AddAsync(_testJewelry);
        await Context.Collections.AddAsync(_testCollection);
        await Context.SaveChangesAsync();
        Context.ChangeTracker.Clear();
    }

    public async Task DisposeAsync()
    {
        Context.Collections.RemoveRange(await Context.Collections.ToListAsync());
        Context.Jewelries.RemoveRange(await Context.Jewelries.ToListAsync());
        await Context.SaveChangesAsync();
    }

    [Fact]
    public async Task CreateCollection_ShouldSucceed()
    {
        var request = new CreateCollectionRequest("Summer Vibes");

        var response = await Client.PostAsJsonAsync(BaseRoute, request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var created = await response.ToResponseModel<CollectionResponse>();
        
        var dbCollection = await Context.Collections.FindAsync(new CollectionId(created.Id));
        dbCollection.Should().NotBeNull();
        dbCollection!.Title.Should().Be("Summer Vibes");
    }

    [Fact]
    public async Task AddJewelryToCollection_ShouldSucceed()
    {
        var request = new AddJewelryToCollectionRequest(_testJewelry.Id.Value);

        var response = await Client.PostAsJsonAsync($"{BaseRoute}/{_testCollection.Id.Value}/jewelries", request);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var dbCollection = await Context.Collections
            .Include(c => c.Jewelries)
            .FirstOrDefaultAsync(c => c.Id == _testCollection.Id);

        dbCollection!.Jewelries.Should().Contain(j => j.Id == _testJewelry.Id);
    }

    [Fact]
    public async Task GetAllCollections_ShouldReturnList()
    {
        var response = await Client.GetAsync(BaseRoute);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var items = await response.ToResponseModel<List<CollectionResponse>>();
        items.Should().Contain(c => c.Title == _testCollection.Title);
    }

    [Fact]
    public async Task GetCollectionById_ShouldReturnWithJewelries()
    {
        var collection = await Context.Collections.FindAsync(_testCollection.Id);
        var jewelry = await Context.Jewelries.FindAsync(_testJewelry.Id);
        collection!.Jewelries.Add(jewelry!);
        await Context.SaveChangesAsync();
        Context.ChangeTracker.Clear();

        var response = await Client.GetAsync($"{BaseRoute}/{_testCollection.Id.Value}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var item = await response.ToResponseModel<CollectionResponse>();
        item.Title.Should().Be(_testCollection.Title);
        item.JewelryIds.Should().Contain(_testJewelry.Id.Value);
    }
}
