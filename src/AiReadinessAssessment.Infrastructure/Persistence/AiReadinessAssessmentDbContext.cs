using AiReadinessAssessment.Domain.InitialAssessment;
using AiReadinessAssessment.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AiReadinessAssessment.Infrastructure.Persistence;

/// <summary>
/// Entity Framework Core DbContext for AI Readiness Assessment Portal.
/// Manages the database connection and entity mapping configuration.
/// </summary>
public class AiReadinessAssessmentDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the AiReadinessAssessmentDbContext class.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public AiReadinessAssessmentDbContext(DbContextOptions<AiReadinessAssessmentDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the Assessments DbSet.
    /// </summary>
    public DbSet<BaselineAssessment> Assessments { get; set; } = null!;

    /// <summary>
    /// Configures the model for the database context.
    /// Applies all entity type configurations.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply only the root entity configuration
        // Nested owned entities (CategoryAssessment, AssessmentResponse, Recommendation)
        // are already configured via OwnsMany() chains in InitialAssessmentConfiguration
        modelBuilder.ApplyConfiguration(new InitialAssessmentConfiguration());
    }
}
