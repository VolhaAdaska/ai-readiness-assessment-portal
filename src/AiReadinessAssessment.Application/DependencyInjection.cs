using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.InitialAssessment.Commands;
using AiReadinessAssessment.Application.InitialAssessment.Commands.Handlers;
using AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;
using AiReadinessAssessment.Application.InitialAssessment.Queries;
using AiReadinessAssessment.Application.InitialAssessment.Queries.Handlers;
using AiReadinessAssessment.Application.InitialAssessment.Services;
using AiReadinessAssessment.Application.Organization.Commands;
using AiReadinessAssessment.Application.Organization.Commands.Handlers;
using AiReadinessAssessment.Application.Organization.Dtos.Responses;
using AiReadinessAssessment.Application.Organization.Queries;
using AiReadinessAssessment.Application.Organization.Queries.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace AiReadinessAssessment.Application;

/// <summary>
/// Dependency injection extensions for the Application layer.
/// Registers command handlers, query handlers, and related services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds Application layer services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register services
        services.AddScoped<IRecommendationGenerator, RecommendationGenerator>();

        // Register command handlers
        services.AddScoped<ICommandHandler<CreateInitialAssessmentCommand, CreateInitialAssessmentResponse>, CreateInitialAssessmentCommandHandler>();
        services.AddScoped<ICommandHandler<StartInitialAssessmentCommand, StartAssessmentResponse>, StartInitialAssessmentCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateCategoryAssessmentCommand, UpdateCategoryAssessmentResponse>, UpdateCategoryAssessmentCommandHandler>();
        services.AddScoped<ICommandHandler<CompleteInitialAssessmentCommand, CompleteAssessmentResponse>, CompleteInitialAssessmentCommandHandler>();

        // Register query handlers
        services.AddScoped<IQueryHandler<GetInitialAssessmentQuery, InitialAssessmentResponse>, GetInitialAssessmentQueryHandler>();
        services.AddScoped<IQueryHandler<GetCategoryAssessmentQuery, CategoryAssessmentResponse>, GetCategoryAssessmentQueryHandler>();
        services.AddScoped<IQueryHandler<ListAssessmentsQuery, List<AssessmentSummaryResponse>>, ListAssessmentsQueryHandler>();

        // Organization handlers
        services.AddScoped<ICommandHandler<CreateOrganizationCommand, OrganizationResponse>, CreateOrganizationCommandHandler>();
        services.AddScoped<IQueryHandler<ListOrganizationsQuery, List<OrganizationResponse>>, ListOrganizationsQueryHandler>();

        return services;
    }
}
