using System.Net;
using System.Net.Http.Json;
using Api.Dtos;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using Xunit;

namespace Api.Tests.Integration.Jewelry;

public class JewelryCertificatesControllerTests(IntegrationTestWebFactory factory)
    : BaseIntegrationTest(factory), IAsyncLifetime
{
    private const string BaseRoute = "api/certificates";
    private Domain.Entities.Jewelry _testJewelry = null!;

    public async Task InitializeAsync()
    {
        _testJewelry = Domain.Entities.Jewelry.New(
            JewelryId.New(), 
            "Certified Ring", "Desc", JewelryType.Ring, Material.Platinum, 1000m, DateTime.UtcNow);

        await Context.Jewelries.AddAsync(_testJewelry);
        await Context.SaveChangesAsync();
        Context.ChangeTracker.Clear();
    }

    public async Task DisposeAsync()
    {
        Context.JewelryCertificates.RemoveRange(await Context.JewelryCertificates.ToListAsync());
        Context.Jewelries.RemoveRange(await Context.Jewelries.ToListAsync());
        await Context.SaveChangesAsync();
    }

    [Fact]
    public async Task CreateCertificate_ShouldSucceed()
    {
        var request = new CreateJewelryCertificateRequest(
            _testJewelry.Id.Value, 
            "UA-12345", 
            "GIA");

        var response = await Client.PostAsJsonAsync(BaseRoute, request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var dbCert = await Context.JewelryCertificates.FirstOrDefaultAsync(c => c.CertificateNumber == "UA-12345");
        dbCert.Should().NotBeNull();
        dbCert!.JewelryId.Should().Be(_testJewelry.Id);
    }

    [Fact]
    public async Task CreateCertificate_ShouldFail_WhenJewelryNotFound()
    {
        var request = new CreateJewelryCertificateRequest(
            Guid.NewGuid(), 
            "UA-FAIL", 
            "GIA");

        var response = await Client.PostAsJsonAsync(BaseRoute, request);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetCertificateByJewelryId_ShouldReturnCertificate()
    {
        var cert = JewelryCertificate.New(JewelryCertificateId.New(), _testJewelry.Id, "UA-999", "Center");
        await Context.JewelryCertificates.AddAsync(cert);
        await Context.SaveChangesAsync();

        var response = await Client.GetAsync($"{BaseRoute}/jewelry/{_testJewelry.Id.Value}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var item = await response.ToResponseModel<JewelryCertificateResponse>();
        item.CertificateNumber.Should().Be("UA-999");
        item.JewelryId.Should().Be(_testJewelry.Id.Value);
    }
}
