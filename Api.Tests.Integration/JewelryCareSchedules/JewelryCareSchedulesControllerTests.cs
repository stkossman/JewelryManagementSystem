using System.Net;
using System.Net.Http.Json;
using Api.Dtos;
using Domain.Entities;
using Domain.Entities.JewelryCareSchedules;
using Domain.Enums;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using Tests.Data.Jewelry;
using Tests.Data.JewelryCareSchedules;
using Xunit;

namespace Api.Tests.Integration.JewelryCareSchedules;

public class JewelryCareSchedulesControllerTests(IntegrationTestWebFactory factory)
    : BaseIntegrationTest(factory), IAsyncLifetime
{
    private const string BaseRoute = "api/care-schedules";
    private Domain.Entities.Jewelry _testJewelry = null!;
    private JewelryCareSchedule _testSchedule = null!;
    
    public async Task InitializeAsync()
    {
        _testJewelry = JewelryData.CreateTestJewelry();
        _testSchedule = JewelryCareScheduleData.CreateTestSchedule(_testJewelry.Id);

        await Context.Jewelries.AddAsync(_testJewelry);
        await Context.JewelryCareSchedules.AddAsync(_testSchedule);
        await Context.SaveChangesAsync();
        Context.ChangeTracker.Clear();
    }

    public async Task DisposeAsync()
    {
        Context.JewelryCareSchedules.RemoveRange(await Context.JewelryCareSchedules.ToListAsync());
        Context.Jewelries.RemoveRange(await Context.Jewelries.ToListAsync());
        await Context.SaveChangesAsync();
    }

    [Fact]
    public async Task CreateSchedule_ShouldSucceed_WhenDataIsValid()
    {
        var request = new CreateCareScheduleRequest(_testJewelry.Id.Value, DateTime.UtcNow.AddMonths(2), CareInterval.Weekly, "Weekly check");
        
        var response = await Client.PostAsJsonAsync(BaseRoute, request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
    
    [Fact]
    public async Task GetAllSchedules_ShouldReturnAllItems()
    {
        var response = await Client.GetAsync(BaseRoute);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var items = await response.ToResponseModel<List<CareScheduleResponse>>();
        items.Should().Contain(s => s.Id == _testSchedule.Id.Value);
    }
    
    [Fact]
    public async Task GetScheduleById_ShouldReturnItem_WhenExists()
    {
        var response = await Client.GetAsync($"{BaseRoute}/{_testSchedule.Id.Value}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task UpdateSchedule_ShouldSucceed_WhenDataIsValid()
    {
        var request = new UpdateCareScheduleRequest(DateTime.UtcNow.AddYears(1), CareInterval.Annually, "Annual service");

        var response = await Client.PutAsJsonAsync($"{BaseRoute}/{_testSchedule.Id.Value}", request);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        
        var dbSchedule = await Context.JewelryCareSchedules.FindAsync(_testSchedule.Id);
        dbSchedule!.Interval.Should().Be(CareInterval.Annually);
    }

    [Fact]
    public async Task DeactivateSchedule_ShouldSucceed()
    {
        var response = await Client.DeleteAsync($"{BaseRoute}/{_testSchedule.Id.Value}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        
        var dbSchedule = await Context.JewelryCareSchedules.FindAsync(_testSchedule.Id);
        dbSchedule!.IsActive.Should().BeFalse();
    }
    
    [Fact]
    public async Task ReactivateSchedule_ShouldSucceed()
    {
        var scheduleToDeactivate = await Context.JewelryCareSchedules.FindAsync(_testSchedule.Id);
        scheduleToDeactivate!.Deactivate();
        await Context.SaveChangesAsync();
    
        var reactivateRequest = new ReactivateCareScheduleRequest(DateTime.UtcNow.AddDays(30));

        var response = await Client.PostAsJsonAsync($"{BaseRoute}/{_testSchedule.Id.Value}/reactivate", reactivateRequest);

        var responseBody = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent, $"because the reactivation should succeed. Response: {responseBody}");

        var dbSchedule = await Context.JewelryCareSchedules
            .AsNoTracking() 
            .FirstOrDefaultAsync(s => s.Id == _testSchedule.Id);

        dbSchedule.Should().NotBeNull();
        dbSchedule!.IsActive.Should().BeTrue();
    }
}
