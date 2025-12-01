using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class JewelryCertificateConfiguration : IEntityTypeConfiguration<JewelryCertificate>
{
    public void Configure(EntityTypeBuilder<JewelryCertificate> builder)
    {
        builder.ToTable("jewelry_certificates");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => new JewelryCertificateId(value))
            .ValueGeneratedNever();

        builder.Property(c => c.CertificateNumber)
            .HasColumnName("certificate_number")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.IssuedBy)
            .HasColumnName("issued_by")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.JewelryId)
            .HasColumnName("jewelry_id")
            .HasConversion(
                id => id.Value,
                value => new JewelryId(value))
            .IsRequired();

        builder.HasIndex(c => c.CertificateNumber).IsUnique();
    }
}