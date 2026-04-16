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
    /// Gets or sets the InitialAssessments DbSet.
    /// </summary>
    public DbSet<InitialAssessment> InitialAssessments { get; set; } = null!;

    /// <summary>
    /// Configures the model for the database context.
    /// Applies all entity type configurations.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all entity configurations
        modelBuilder.ApplyConfiguration(new InitialAssessmentConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryAssessmentConfiguration());
        modelBuilder.ApplyConfiguration(new AssessmentResponseConfiguration());
        modelBuilder.ApplyConfiguration(new RecommendationConfiguration());
    }
}
