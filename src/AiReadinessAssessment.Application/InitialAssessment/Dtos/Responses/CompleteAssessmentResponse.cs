using AiReadinessAssessment.Domain.Shared;
using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;

/// <summary>
/// Response model for completing an initial assessment.
/// Includes final scores, readiness level, and generated recommendations.
/// </summary>
public class CompleteAssessmentResponse
{
    /// <summary>
    /// Gets or sets the assessment ID.
    /// </summary>
    public Guid AssessmentId { get; set; }

    /// <summary>
    /// Gets or sets the assessment status (Completed).
    /// </summary>
    public AssessmentStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when assessment was completed.
    /// </summary>
    public DateTime CompletedAt { get; set; }

    /// <summary>
    /// Gets or sets the overall readiness score (0-100).
    /// </summary>
    public double OverallScore { get; set; }

    /// <summary>
    /// Gets or sets the overall readiness level classification.
    /// </summary>
    public ReadinessLevel ReadinessLevel { get; set; }

    /// <summary>
    /// Gets or sets the scores for each category.
    /// </summary>
    public Dictionary<CategoryType, double> CategoryScores { get; set; } = new();

    /// <summary>
    /// Gets or sets the count of generated recommendations.
    /// </summary>
    public int RecommendationCount { get; set; }
}