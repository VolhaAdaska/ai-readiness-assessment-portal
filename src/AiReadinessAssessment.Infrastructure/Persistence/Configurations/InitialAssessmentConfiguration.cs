using AiReadinessAssessment.Domain.InitialAssessment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiReadinessAssessment.Infrastructure.Persistence.Configurations;

/// <summary>
/// Entity Framework Core configuration for the InitialAssessment aggregate root.
/// Configures the table structure, primary key, properties, enums, and owned entity relationships.
/// </summary>
public class InitialAssessmentConfiguration : IEntityTypeConfiguration<BaselineAssessment>
{
    /// <summary>
    /// Configures the InitialAssessment entity mapping.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<BaselineAssessment> builder)
    {
        // Table mapping - clean, professional name
        builder.ToTable("Assessments");

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
                navigationBuilder.ToTable("AssessmentCategories");
                // Explicitly name the shadow FK to use AssessmentId instead of BaselineAssessmentId
                navigationBuilder.WithOwner()
                    .HasForeignKey("AssessmentId");
                
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
                        responseBuilder.ToTable("AssessmentResponses");
                        // Explicitly name the shadow FKs
                        responseBuilder.WithOwner()
                            .HasForeignKey("AssessmentId", "CategoryAssessmentId");
                        
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
                navigationBuilder.ToTable("Recommendations");
                // Explicitly name the shadow FK to use AssessmentId instead of BaselineAssessmentId
                navigationBuilder.WithOwner()
                    .HasForeignKey("AssessmentId");
                
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
