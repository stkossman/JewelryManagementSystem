using System.Net;
using System.Net.Http.Json;
using Api.Dtos;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using Tests.Data.Jewelry;
using Tests.Data.JewelryOrders;
using Xunit;

namespace Api.Tests.Integration.JewelryOrders;

public class JewelryOrdersControllerTests(IntegrationTestWebFactory factory)
    : BaseIntegrationTest(factory), IAsyncLifetime
{
    private const string BaseRoute = "api/jewelry-orders";
    private Domain.Entities.Jewelry _testJewelry = null!;
    private JewelryOrder _testOrder = null!;

    public async Task InitializeAsync()
    {
        _testJewelry = JewelryData.CreateTestJewelry();
        _testOrder = JewelryOrderData.CreateTestOrder(_testJewelry.Id);

        await Context.Jewelries.AddAsync(_testJewelry);
        await Context.JewelryOrders.AddAsync(_testOrder);
        await Context.SaveChangesAsync();
        Context.ChangeTracker.Clear();
    }

    public async Task DisposeAsync()
    {
        Context.JewelryOrders.RemoveRange(await Context.JewelryOrders.ToListAsync());
        Context.Jewelries.RemoveRange(await Context.Jewelries.ToListAsync());
        await Context.SaveChangesAsync();
    }

    [Fact]
    public async Task CreateOrder_ShouldSucceed_WhenDataIsValid()
    {
        var request = new CreateJewelryOrderRequest($"ORD-{Guid.NewGuid():N}", _testJewelry.Id.Value, "Jane Smith", "Birthday gift.", OrderPriority.High, DateTime.UtcNow.AddDays(10));
        
        var response = await Client.PostAsJsonAsync(BaseRoute, request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var createdDto = await response.ToResponseModel<JewelryOrderResponse>();
        var dbOrder = await Context.JewelryOrders.FindAsync(new JewelryOrderId(createdDto.Id)); 
        
        dbOrder.Should().NotBeNull();
        dbOrder!.CustomerName.Should().Be("Jane Smith");
    }

    [Fact]
    public async Task GetAllOrders_ShouldReturnAllItems()
    {
        var response = await Client.GetAsync(BaseRoute);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var items = await response.ToResponseModel<List<JewelryOrderResponse>>();
        items.Should().Contain(o => o.Id == _testOrder.Id.Value);
    }
    
    [Fact]
    public async Task GetOrderById_ShouldReturnItem_WhenExists()
    {
        var response = await Client.GetAsync($"{BaseRoute}/{_testOrder.Id.Value}"); 

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var item = await response.ToResponseModel<JewelryOrderResponse>();
        item.Id.Should().Be(_testOrder.Id.Value);
    }
    
    [Fact]
    public async Task UpdateOrder_ShouldSucceed_WhenDataIsValid()
    {
        var request = new UpdateJewelryOrderRequest("Updated Customer", "Updated notes.", OrderPriority.Low, DateTime.UtcNow.AddDays(5));

        var response = await Client.PutAsJsonAsync($"{BaseRoute}/{_testOrder.Id.Value}", request);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        
        var dbOrder = await Context.JewelryOrders.FindAsync(_testOrder.Id);
        dbOrder!.CustomerName.Should().Be("Updated Customer");
    }

    [Fact]
    public async Task CancelOrder_ShouldChangeStatusToCancelled()
    {
        var response = await Client.PostAsync($"{BaseRoute}/{_testOrder.Id.Value}/cancel", null);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var dbOrder = await Context.JewelryOrders.FindAsync(_testOrder.Id);
        dbOrder!.Status.Should().Be(OrderStatus.Cancelled);
    }
}
