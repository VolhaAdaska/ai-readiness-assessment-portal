using AiReadinessAssessment.Domain.InitialAssessment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiReadinessAssessment.Infrastructure.Persistence.Configurations;

/// <summary>
/// Entity Framework Core configuration for the InitialAssessment aggregate root.
/// Configures the table structure, primary key, properties, enums, and owned entity relationships.
/// </summary>
public class InitialAssessmentConfiguration : IEntityTypeConfiguration<InitialAssessment>
{
    /// <summary>
    /// Configures the InitialAssessment entity mapping.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<InitialAssessment> builder)
    {
        // Table mapping
        builder.ToTable("InitialAssessments");

        // Primary key
        builder.HasKey(a => a.Id);

        // Properties
        builder.Property(a => a.Id)
            .HasColumnName("Id")
            .ValueGeneratedNever();

        builder.Property(a => a.OrganizationId)
            .HasColumnName("OrganizationId")
            .IsRequired();

        builder.Property(a => a.Status)
            .HasColumnName("Status")
            .IsRequired()
            .HasConversion(
                v => (int)v,
                v => (AssessmentStatus)v);

        builder.Property(a => a.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired();

        builder.Property(a => a.StartedAt)
            .HasColumnName("StartedAt");

        builder.Property(a => a.CompletedAt)
            .HasColumnName("CompletedAt");

        // Owned collections - CategoryAssessments
        builder.OwnsMany(
            a => a.CategoryAssessments,
            navigationBuilder =>
            {
                navigationBuilder.ToTable("InitialAssessments_CategoryAssessments");
                navigationBuilder.HasKey("InitialAssessmentId", "Id");
                
                navigationBuilder.Property(c => c.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever();

                navigationBuilder.Property(c => c.Category)
                    .HasColumnName("Category")
                    .IsRequired()
                    .HasConversion(
                        v => (int)v,
                        v => (CategoryType)v);

                navigationBuilder.Property(c => c.MaturityLevel)
                    .HasColumnName("MaturityLevel")
                    .IsRequired();

                navigationBuilder.Property(c => c.Observations)
                    .HasColumnName("Observations")
                    .HasMaxLength(1000);

                // Nested owned collection - AssessmentResponses
                navigationBuilder.OwnsMany(
                    c => c.Responses,
                    responseBuilder =>
                    {
                        responseBuilder.ToTable("InitialAssessments_CategoryAssessments_Responses");
                        responseBuilder.HasKey("InitialAssessmentId", "CategoryAssessmentId", "Id");

                        responseBuilder.Property(r => r.Id)
                            .HasColumnName("Id")
                            .ValueGeneratedNever();

                        responseBuilder.Property(r => r.QuestionId)
                            .HasColumnName("QuestionId")
                            .IsRequired();

                        responseBuilder.Property(r => r.Answer)
                            .HasColumnName("Answer")
                            .IsRequired()
                            .HasMaxLength(2000);

                        responseBuilder.Property(r => r.RecordedAt)
                            .HasColumnName("RecordedAt")
                            .IsRequired();
                    });
            });

        // Owned collections - Recommendations
        builder.OwnsMany(
            a => a.Recommendations,
            navigationBuilder =>
            {
                navigationBuilder.ToTable("InitialAssessments_Recommendations");
                navigationBuilder.HasKey("InitialAssessmentId", "Id");

                navigationBuilder.Property(r => r.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever();

                navigationBuilder.Property(r => r.Category)
                    .HasColumnName("Category")
                    .IsRequired()
                    .HasConversion(
                        v => (int)v,
                        v => (CategoryType)v);

                navigationBuilder.Property(r => r.Priority)
                    .HasColumnName("Priority")
                    .IsRequired()
                    .HasConversion(
                        v => (int)v,
                        v => (Priority)v);

                navigationBuilder.Property(r => r.Title)
                    .HasColumnName("Title")
                    .IsRequired()
                    .HasMaxLength(255);

                navigationBuilder.Property(r => r.Description)
                    .HasColumnName("Description")
                    .IsRequired()
                    .HasMaxLength(2000);

                navigationBuilder.Property(r => r.CreatedAt)
                    .HasColumnName("CreatedAt")
                    .IsRequired();
            });
    }
}
