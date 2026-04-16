using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.InitialAssessment.Services;

/// <summary>
/// Service interface for generating recommendations based on assessment data.
/// Implementation may be in Application or Infrastructure layer.
/// </summary>
public interface IRecommendationGenerator
{
    /// <summary>
    /// Generates recommendations for a completed assessment.
    /// Based on category scores, maturity levels, and domain expertise rules.
    /// </summary>
    /// <param name="assessment">The completed assessment.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of generated recommendations for the assessment.</returns>
    Task<List<Recommendation>> GenerateRecommendationsAsync(
        AiReadinessAssessment.Domain.InitialAssessment.InitialAssessment assessment,
        CancellationToken cancellationToken = default);
}