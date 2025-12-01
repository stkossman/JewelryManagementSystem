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

namespace Api.Tests.Integration.Jewelry;

public class JewelryControllerTests(IntegrationTestWebFactory factory)
    : BaseIntegrationTest(factory), IAsyncLifetime
{
    private const string BaseRoute = "api/jewelry";
    private Domain.Entities.Jewelry _testJewelry = null!;

    public async Task InitializeAsync()
    {
        _testJewelry = JewelryData.CreateTestJewelry();
    
        await Context.Jewelries.AddAsync(_testJewelry);
        await Context.SaveChangesAsync();
        Context.ChangeTracker.Clear();
    }

    public async Task DisposeAsync()
    {
        Context.JewelryCertificates.RemoveRange(await Context.JewelryCertificates.ToListAsync());
        Context.Collections.RemoveRange(await Context.Collections.ToListAsync());
        Context.Jewelries.RemoveRange(await Context.Jewelries.ToListAsync());
        await Context.SaveChangesAsync();
    }

    [Fact]
    public async Task CreateJewelry_ShouldSucceed_WhenDataIsValid()
    {
        var request = new CreateJewelryRequest("Unique Diamond Ring", "A newly created diamond ring.", JewelryType.Ring, Material.Platinum, 2500.0m);

        var response = await Client.PostAsJsonAsync(BaseRoute, request);

        var responseBody = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.Created, $"because the request is valid. Response: {responseBody}");

        var createdDto = await response.ToResponseModel<JewelryResponse>();
        var dbJewelry = await Context.Jewelries.FindAsync(new JewelryId(createdDto.Id)); 
        
        dbJewelry.Should().NotBeNull();
        dbJewelry!.Name.Should().Be(request.Name);
        dbJewelry!.Description.Should().Be(request.Description);
        dbJewelry!.JewelryType.Should().Be(request.JewelryType);
    }

    [Fact]
    public async Task GetAllJewelry_ShouldReturnAllItems()
    {
        var response = await Client.GetAsync(BaseRoute);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var items = await response.ToResponseModel<List<JewelryResponse>>();
        items.Should().Contain(j => j.Id == _testJewelry.Id.Value);
    }
    
    [Fact]
    public async Task GetJewelryById_ShouldReturnItem_WhenExists()
    {
        var response = await Client.GetAsync($"{BaseRoute}/{_testJewelry.Id.Value}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var item = await response.ToResponseModel<JewelryResponse>();
        
        item.Id.Should().Be(_testJewelry.Id.Value);
        item.Name.Should().Be(_testJewelry.Name);
    }
    
    [Fact]
    public async Task GetJewelryById_ShouldReturnNotFound_WhenNotExists()
    {
        var response = await Client.GetAsync($"{BaseRoute}/{Guid.NewGuid()}");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateJewelry_ShouldSucceed_WhenDataIsValid()
    {
        var request = new UpdateJewelryRequest("Updated Ring Name", "This description has been updated.", JewelryType.Ring, Material.Silver, 550m);

        var response = await Client.PutAsJsonAsync($"{BaseRoute}/{_testJewelry.Id.Value}", request);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        
        var dbJewelry = await Context.Jewelries.FindAsync(_testJewelry.Id);
        dbJewelry!.Name.Should().Be("Updated Ring Name");
        dbJewelry.Material.Should().Be(Material.Silver);
    }

    [Fact]
    public async Task UpdateJewelryStatus_ShouldChangeStatus()
    {
        var request = new UpdateJewelryStatusRequest(JewelryStatus.Sold);

        var response = await Client.PatchAsJsonAsync($"{BaseRoute}/{_testJewelry.Id.Value}/status", request);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var dbJewelry = await Context.Jewelries.FindAsync(_testJewelry.Id);
        dbJewelry!.Status.Should().Be(JewelryStatus.Sold);
    }
    
    [Fact]
    public async Task GetJewelryById_ShouldReturnItem_WithCertificateAndCollections()
    {
        var cert = JewelryCertificateData.CreateTestCertificate(_testJewelry.Id);
        var collection = CollectionData.CreateTestCollection();
        
        await Context.JewelryCertificates.AddAsync(cert);
        await Context.Collections.AddAsync(collection);
        
        var jewelryFromDb = await Context.Jewelries.FindAsync(_testJewelry.Id);
        jewelryFromDb!.AddToCollection(collection);
        
        await Context.SaveChangesAsync();
        Context.ChangeTracker.Clear();

        var response = await Client.GetAsync($"{BaseRoute}/{_testJewelry.Id.Value}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var item = await response.ToResponseModel<JewelryResponse>();
        
        item.Id.Should().Be(_testJewelry.Id.Value);
        item.Certificate.Should().NotBeNull();
        item.Certificate!.CertificateNumber.Should().Be(cert.CertificateNumber);
        item.Collections.Should().Contain(collection.Title);
    }
}
