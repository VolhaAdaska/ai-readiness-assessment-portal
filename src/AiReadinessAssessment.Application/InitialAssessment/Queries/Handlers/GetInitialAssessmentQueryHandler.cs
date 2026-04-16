using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.Common.Exceptions;
using AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;
using AiReadinessAssessment.Application.InitialAssessment.Queries;
using AiReadinessAssessment.Application.InitialAssessment.Services;

namespace AiReadinessAssessment.Application.InitialAssessment.Queries.Handlers;

/// <summary>
/// Handler for the GetInitialAssessmentQuery.
/// Retrieves a complete initial assessment with all category assessments and recommendations.
/// </summary>
public class GetInitialAssessmentQueryHandler : IQueryHandler<GetInitialAssessmentQuery, InitialAssessmentResponse>
{
    private readonly IInitialAssessmentRepository _repository;

    /// <summary>
    /// Initializes a new instance of the GetInitialAssessmentQueryHandler class.
    /// </summary>
    /// <param name="repository">The assessment repository.</param>
    public GetInitialAssessmentQueryHandler(IInitialAssessmentRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Handles the get assessment query.
    /// </summary>
    /// <param name="query">The query containing the assessment ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Response containing the assessment details with all categories and recommendations.</returns>
    /// <exception cref="NotFoundException">Thrown if assessment not found.</exception>
    public async Task<InitialAssessmentResponse> Handle(
        GetInitialAssessmentQuery query,
        CancellationToken cancellationToken = default)
    {
        // Retrieve assessment with all related entities
        var assessment = await _repository.GetByIdAsync(query.AssessmentId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.InitialAssessment.InitialAssessment), query.AssessmentId);

        // Map category assessments to response DTOs
        var categoryResponses = assessment.CategoryAssessments
            .Select(c => new CategoryAssessmentResponse
            {
                Id = c.Id,
                Category = c.Category,
                MaturityLevel = c.MaturityLevel,
                Observations = c.Observations,
                CategoryScore = c.CalculateCategoryScore(),
                IsComplete = c.IsComplete()
            })
            .ToList();

        // Map recommendations to response DTOs
        var recommendationResponses = assessment.Recommendations
            .Select(r => new RecommendationResponse
            {
                Id = r.Id,
                Category = r.Category,
                Priority = r.Priority,
                Title = r.Title,
                Description = r.Description,
                CreatedAt = r.CreatedAt
            })
            .ToList();

        // Map to response DTO
        return new InitialAssessmentResponse
        {
            Id = assessment.Id,
            OrganizationId = assessment.OrganizationId,
            Status = assessment.Status,
            CreatedAt = assessment.CreatedAt,
            StartedAt = assessment.StartedAt,
            CompletedAt = assessment.CompletedAt,
            OverallScore = assessment.CalculateOverallReadinessScore(),
            ReadinessLevel = assessment.Status == Domain.InitialAssessment.AssessmentStatus.Completed
                ? assessment.DetermineReadinessLevel()
                : null,
            Categories = categoryResponses,
            Recommendations = recommendationResponses
        };
    }
}
