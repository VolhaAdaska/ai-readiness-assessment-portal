using AiReadinessAssessment.Domain.InitialAssessment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiReadinessAssessment.Infrastructure.Persistence.Configurations;

/// <summary>
/// Entity Framework Core configuration for the CategoryAssessment owned entity.
/// Configures the properties and owned collection of assessment responses.
/// </summary>
public class CategoryAssessmentConfiguration : IEntityTypeConfiguration<CategoryAssessment>
{
    /// <summary>
    /// Configures the CategoryAssessment entity mapping.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<CategoryAssessment> builder)
    {
        // Note: This configuration is applied as a reference configuration,
        // though the primary configuration happens through InitialAssessmentConfiguration.OwnsMany().
        // This separate configuration class provides clarity and maintainability.

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Category)
            .IsRequired()
            .HasConversion(
                v => (int)v,
                v => (CategoryType)v);

        builder.Property(c => c.MaturityLevel)
            .IsRequired();

        builder.Property(c => c.Observations)
            .HasMaxLength(1000);
    }
}
