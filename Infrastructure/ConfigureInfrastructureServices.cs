using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Queries;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureInfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IJewelryRepository, JewelryRepository>();
        services.AddScoped<IJewelryOrderRepository, JewelryOrderRepository>();
        services.AddScoped<IJewelryCareScheduleRepository, JewelryCareScheduleRepository>();
        services.AddScoped<IJewelryCertificateRepository, JewelryCertificateRepository>();
        services.AddScoped<ICollectionRepository, CollectionRepository>();

        services.AddScoped<IJewelryQueries, JewelryQueries>();
        services.AddScoped<IJewelryOrderQueries, JewelryOrderQueries>();
        services.AddScoped<IJewelryCareScheduleQueries, JewelryCareScheduleQueries>();
        services.AddScoped<IJewelryCertificateQueries, JewelryCertificateQueries>();
        services.AddScoped<ICollectionQueries, CollectionQueries>();

        return services;
    }
}