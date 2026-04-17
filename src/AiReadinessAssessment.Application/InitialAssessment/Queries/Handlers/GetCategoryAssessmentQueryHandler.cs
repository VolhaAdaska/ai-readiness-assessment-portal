using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.Common.Exceptions;
using AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;
using AiReadinessAssessment.Application.InitialAssessment.Queries;
using AiReadinessAssessment.Application.InitialAssessment.Services;

namespace AiReadinessAssessment.Application.InitialAssessment.Queries.Handlers;

/// <summary>
/// Handler for the GetCategoryAssessmentQuery.
/// Retrieves a specific category assessment from an initial assessment.
/// </summary>
public class GetCategoryAssessmentQueryHandler : IQueryHandler<GetCategoryAssessmentQuery, CategoryAssessmentResponse>
{
    private readonly IInitialAssessmentRepository _repository;

    /// <summary>
    /// Initializes a new instance of the GetCategoryAssessmentQueryHandler class.
    /// </summary>
    /// <param name="repository">The assessment repository.</param>
    public GetCategoryAssessmentQueryHandler(IInitialAssessmentRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Handles the get category assessment query.
    /// </summary>
    /// <param name="query">The query containing the assessment ID and category type.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Response containing the category assessment details.</returns>
    /// <exception cref="NotFoundException">Thrown if assessment or category not found.</exception>
    public async Task<CategoryAssessmentResponse> Handle(
        GetCategoryAssessmentQuery query,
        CancellationToken cancellationToken = default)
    {
        // Retrieve assessment
        var assessment = await _repository.GetByIdAsync(query.AssessmentId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.InitialAssessment.BaselineAssessment), query.AssessmentId);

        // Find the category assessment
        var categoryAssessment = assessment.CategoryAssessments
            .FirstOrDefault(c => c.Category == query.Category)
            ?? throw new NotFoundException($"Category assessment for {query.Category} not found in assessment {query.AssessmentId}");

        // Map to response DTO
        return new CategoryAssessmentResponse
        {
            Id = categoryAssessment.Id,
            Category = categoryAssessment.Category,
            MaturityLevel = categoryAssessment.MaturityLevel,
            Observations = categoryAssessment.Observations,
            CategoryScore = categoryAssessment.CalculateCategoryScore(),
            IsComplete = categoryAssessment.IsComplete()
        };
    }
}
