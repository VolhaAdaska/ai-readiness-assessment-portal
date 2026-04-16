using AiReadinessAssessment.Domain.Shared;
using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;

/// <summary>
/// Complete response model for an initial assessment.
/// Includes all assessment data, categories, scores, and recommendations.
/// </summary>
public class InitialAssessmentResponse
{
    /// <summary>
    /// Gets or sets the assessment ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the organization ID.
    /// </summary>
    public Guid OrganizationId { get; set; }

    /// <summary>
    /// Gets or sets the assessment status.
    /// </summary>
    public AssessmentStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when assessment was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when assessment was started, if applicable.
    /// </summary>
    public DateTime? StartedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when assessment was completed, if applicable.
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Gets or sets the overall readiness score (0-100), or 0 if not completed.
    /// </summary>
    public double OverallScore { get; set; }

    /// <summary>
    /// Gets or sets the overall readiness level, or null if not completed.
    /// </summary>
    public ReadinessLevel? ReadinessLevel { get; set; }

    /// <summary>
    /// Gets or sets the collection of category assessments.
    /// </summary>
    public List<CategoryAssessmentResponse> Categories { get; set; } = new();

    /// <summary>
    /// Gets or sets the collection of recommendations generated from the assessment.
    /// </summary>
    public List<RecommendationResponse> Recommendations { get; set; } = new();
}