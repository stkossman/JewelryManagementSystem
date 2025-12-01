using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Domain.Entities.JewelryCareSchedules;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Jewelry> Jewelries => Set<Jewelry>();
    public DbSet<JewelryCertificate> JewelryCertificates => Set<JewelryCertificate>();
    public DbSet<Collection> Collections => Set<Collection>();
    public DbSet<JewelryOrder> JewelryOrders => Set<JewelryOrder>();
    public DbSet<JewelryCareSchedule> JewelryCareSchedules => Set<JewelryCareSchedule>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}