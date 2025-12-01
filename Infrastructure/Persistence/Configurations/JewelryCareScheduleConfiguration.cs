using Domain.Entities;
using Domain.Entities.JewelryCareSchedules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class JewelryCareScheduleConfiguration : IEntityTypeConfiguration<JewelryCareSchedule>
{
    public void Configure(EntityTypeBuilder<JewelryCareSchedule> builder)
    {
        builder.ToTable("jewelry_care_schedules");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => new JewelryCareScheduleId(value))
            .ValueGeneratedNever();

        builder.Property(s => s.JewelryId)
            .HasColumnName("jewelry_id")
            .HasConversion(
                id => id.Value,
                value => new JewelryId(value))
            .IsRequired();

        builder.Property(s => s.NextServiceDate)
            .HasColumnName("next_service_date")
            .IsRequired();

        builder.Property(s => s.Interval)
            .HasColumnName("interval")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(s => s.Description)
            .HasColumnName("description")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(s => s.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.Property(s => s.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(s => s.UpdatedAt)
            .HasColumnName("updated_at");

        builder.HasIndex(s => s.JewelryId);
        builder.HasIndex(s => s.NextServiceDate);
        builder.HasIndex(s => s.IsActive);
    }
}