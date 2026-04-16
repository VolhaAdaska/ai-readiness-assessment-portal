using AiReadinessAssessment.Domain.InitialAssessment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiReadinessAssessment.Infrastructure.Persistence.Configurations;

/// <summary>
/// Entity Framework Core configuration for the Recommendation owned entity.
/// Configures the properties for recommendations generated from an assessment.
/// </summary>
public class RecommendationConfiguration : IEntityTypeConfiguration<Recommendation>
{
    /// <summary>
    /// Configures the Recommendation entity mapping.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Recommendation> builder)
    {
        // Note: This configuration is applied as a reference configuration,
        // though the primary configuration happens through InitialAssessmentConfiguration.OwnsMany().
        // This separate configuration class provides clarity and maintainability.

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .ValueGeneratedNever();

        builder.Property(r => r.Category)
            .IsRequired()
            .HasConversion(
                v => (int)v,
                v => (CategoryType)v);

        builder.Property(r => r.Priority)
            .IsRequired()
            .HasConversion(
                v => (int)v,
                v => (Priority)v);

        builder.Property(r => r.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(r => r.CreatedAt)
            .IsRequired();
    }
}
