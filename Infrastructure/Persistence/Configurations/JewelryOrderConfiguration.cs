using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class JewelryOrderConfiguration : IEntityTypeConfiguration<JewelryOrder>
{
    public void Configure(EntityTypeBuilder<JewelryOrder> builder)
    {
        builder.ToTable("jewelry_orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => new JewelryOrderId(value))
            .ValueGeneratedNever();

        builder.Property(o => o.OrderNumber)
            .HasColumnName("order_number")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(o => o.JewelryId)
            .HasColumnName("jewelry_id")
            .HasConversion(
                id => id.Value,
                value => new JewelryId(value))
            .IsRequired();

        builder.Property(o => o.CustomerName)
            .HasColumnName("customer_name")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(o => o.Notes)
            .HasColumnName("notes")
            .HasMaxLength(1000);

        builder.Property(o => o.Priority)
            .HasColumnName("priority")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(o => o.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(o => o.ScheduledDate)
            .HasColumnName("scheduled_date")
            .IsRequired();

        builder.Property(o => o.CompletedAt)
            .HasColumnName("completed_at");

        builder.Property(o => o.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(o => o.UpdatedAt)
            .HasColumnName("updated_at");

        builder.HasIndex(o => o.OrderNumber).IsUnique();
        builder.HasIndex(o => o.JewelryId);
        builder.HasIndex(o => o.Status);
        builder.HasIndex(o => o.ScheduledDate);
    }
}