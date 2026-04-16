using AiReadinessAssessment.Domain.InitialAssessment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiReadinessAssessment.Infrastructure.Persistence.Configurations;

/// <summary>
/// Entity Framework Core configuration for the AssessmentResponse owned entity.
/// Configures the properties for Q&A responses within a category assessment.
/// </summary>
public class AssessmentResponseConfiguration : IEntityTypeConfiguration<AssessmentResponse>
{
    /// <summary>
    /// Configures the AssessmentResponse entity mapping.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<AssessmentResponse> builder)
    {
        // Note: This configuration is applied as a reference configuration,
        // though the primary configuration happens through InitialAssessmentConfiguration.OwnsMany() → OwnsMany().
        // This separate configuration class provides clarity and maintainability.

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .ValueGeneratedNever();

        builder.Property(r => r.QuestionId)
            .IsRequired();
        // QuestionId is an external reference (no foreign key to Questions table;
        // Questions are managed in a separate bounded context)

        builder.Property(r => r.Answer)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(r => r.RecordedAt)
            .IsRequired();
    }
}
