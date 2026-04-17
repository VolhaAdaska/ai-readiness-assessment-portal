using AiReadinessAssessment.Domain.Organization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiReadinessAssessment.Infrastructure.Persistence.Configurations;

/// <summary>
/// Entity Framework Core configuration for the Organization entity.
/// </summary>
public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    /// <summary>
    /// Configures the Organization entity mapping.
    /// </summary>
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable("Organizations");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .HasColumnName("Id")
            .ValueGeneratedNever();

        builder.Property(o => o.Name)
            .HasColumnName("Name")
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(o => o.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired();
    }
}
