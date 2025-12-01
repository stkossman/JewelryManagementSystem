using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Tests.Common;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebFactory>, IDisposable
{
    private readonly IServiceScope _scope;
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext Context;

    protected BaseIntegrationTest(IntegrationTestWebFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Client = factory.CreateClient();
        Context = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        Context.Database.Migrate();
    }
    
    public void Dispose()
    {
        _scope?.Dispose();
        Context?.Dispose();
    }
}