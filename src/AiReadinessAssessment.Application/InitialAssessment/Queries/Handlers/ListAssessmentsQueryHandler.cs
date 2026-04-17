using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;
using AiReadinessAssessment.Application.InitialAssessment.Services;
using AiReadinessAssessment.Application.Organization.Services;
using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.InitialAssessment.Queries.Handlers;

/// <summary>
/// Handler for ListAssessmentsQuery.
/// Returns a lightweight summary of every assessment for the dashboard.
/// </summary>
public class ListAssessmentsQueryHandler : IQueryHandler<ListAssessmentsQuery, List<AssessmentSummaryResponse>>
{
    private readonly IInitialAssessmentRepository _repository;
    private readonly IOrganizationRepository _organizationRepository;

    /// <summary>
    /// Initializes a new instance of the ListAssessmentsQueryHandler class.
    /// </summary>
    /// <param name="repository">The assessment repository.</param>
    /// <param name="organizationRepository">The organization repository.</param>
    public ListAssessmentsQueryHandler(
        IInitialAssessmentRepository repository,
        IOrganizationRepository organizationRepository)
    {
        _repository = repository;
        _organizationRepository = organizationRepository;
    }

    /// <summary>
    /// Handles the list assessments query.
    /// </summary>
    /// <param name="query">The query (no parameters required).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of assessment summary items ordered by creation date descending.</returns>
    public async Task<List<AssessmentSummaryResponse>> Handle(
        ListAssessmentsQuery query,
        CancellationToken cancellationToken = default)
    {
        var assessments = await _repository.GetAllAsync(cancellationToken);
        var organizations = await _organizationRepository.GetAllAsync(cancellationToken);

        // Build a lookup to avoid repeated searches — single pass, O(1) per assessment
        var organizationNames = organizations.ToDictionary(o => o.Id, o => o.Name);

        return assessments
            .OrderByDescending(a => a.CreatedAt)
            .Select(a => MapToSummary(a, organizationNames))
            .ToList();
    }

    private static AssessmentSummaryResponse MapToSummary(
        BaselineAssessment assessment,
        Dictionary<Guid, string> organizationNames)
    {
        var totalCategories = assessment.CategoryAssessments.Count;
        var completedCategories = assessment.CategoryAssessments.Count(c => c.IsComplete());
        var completionPercentage = totalCategories > 0
            ? (int)Math.Round((double)completedCategories / totalCategories * 100)
            : 0;

        var isCompleted = assessment.Status == AssessmentStatus.Completed;

        return new AssessmentSummaryResponse
        {
            AssessmentId = assessment.Id,
            OrganizationId = assessment.OrganizationId,
            OrganizationName = organizationNames.GetValueOrDefault(assessment.OrganizationId, string.Empty),
            Status = assessment.Status,
            CreatedAt = assessment.CreatedAt,
            StartedAt = assessment.StartedAt,
            CompletedAt = assessment.CompletedAt,
            ReadinessLevel = isCompleted ? assessment.DetermineReadinessLevel() : null,
            OverallScore = isCompleted ? assessment.CalculateOverallReadinessScore() : null,
            TotalCategories = totalCategories,
            CompletedCategories = completedCategories,
            CompletionPercentage = completionPercentage
        };
    }
}
