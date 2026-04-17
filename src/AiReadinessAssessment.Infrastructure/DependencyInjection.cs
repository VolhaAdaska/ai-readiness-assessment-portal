using AiReadinessAssessment.Application.InitialAssessment.Services;
using AiReadinessAssessment.Application.Organization.Services;
using AiReadinessAssessment.Infrastructure.Persistence;
using AiReadinessAssessment.Infrastructure.Persistence.Repositories;
using AiReadinessAssessment.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AiReadinessAssessment.Infrastructure;

/// <summary>
/// Dependency injection extensions for the Infrastructure layer.
/// Registers DbContext and repository implementations.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds Infrastructure layer services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="connectionString">The SQLite database connection string.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        // Register DbContext with SQLite
        services.AddDbContext<AiReadinessAssessmentDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        // Register repositories
        services.AddScoped<IInitialAssessmentRepository, InitialAssessmentRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();

        // Register application services
        services.AddScoped<IRecommendationGenerator, RuleBasedRecommendationGenerator>();

        return services;
    }
}
