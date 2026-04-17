using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AiReadinessAssessment.Infrastructure.Persistence;

/// <summary>
/// Design-time DbContext factory for EF Core migration tools.
/// Provides a DbContext instance when running migrations from the command line.
/// This allows migrations to work without requiring the startup project to provide configuration.
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AiReadinessAssessmentDbContext>
{
    /// <summary>
    /// Creates a DbContext instance at design-time for migrations.
    /// </summary>
    /// <param name="args">Command-line arguments (unused).</param>
    /// <returns>A configured AiReadinessAssessmentDbContext instance.</returns>
    public AiReadinessAssessmentDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AiReadinessAssessmentDbContext>();
        
        // Use a default SQLite database file for design-time migrations
        const string connectionString = "Data Source=ai_readiness_assessment.db";
        
        optionsBuilder.UseSqlite(connectionString);

        return new AiReadinessAssessmentDbContext(optionsBuilder.Options);
    }
}
