using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class JewelryConfiguration : IEntityTypeConfiguration<Jewelry>
{
    public void Configure(EntityTypeBuilder<Jewelry> builder)
    {
        builder.ToTable("jewelries");

        builder.HasKey(j => j.Id);

        builder.Property(j => j.Id)
            .HasColumnName("id")
            .HasConversion(id => id.Value,
                value => new JewelryId(value))
            .ValueGeneratedNever();

        builder.Property(j => j.Name)
            .HasColumnName("name")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(j => j.Description)
            .HasColumnName("description")
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(j => j.JewelryType)
            .HasColumnName("jewelry_type")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(j => j.Material)
            .HasColumnName("material")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(j => j.Price)
            .HasColumnName("price")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(j => j.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(j => j.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(j => j.UpdatedAt)
            .HasColumnName("updated_at");
        
        builder.HasOne(j => j.Certificate)
            .WithOne(c => c.Jewelry)
            .HasForeignKey<JewelryCertificate>(c => c.JewelryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(j => j.Collections)
            .WithMany(c => c.Jewelries)
            .UsingEntity<Dictionary<string, object>>(
                "jewelry_collections",
                j => j.HasOne<Collection>().WithMany().HasForeignKey("collection_id"),
                c => c.HasOne<Jewelry>().WithMany().HasForeignKey("jewelry_id")
            );

        builder.HasIndex(j => j.Status);
        builder.HasIndex(j => j.JewelryType);
    }
}