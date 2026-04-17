using AiReadinessAssessment.Domain.InitialAssessment;
using AiReadinessAssessment.Domain.Shared;

namespace AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;

/// <summary>
/// Lightweight assessment summary for dashboard listing.
/// Contains enough data to display progress and continue working on an assessment.
/// </summary>
public class AssessmentSummaryResponse
{
    /// <summary>
    /// Unique identifier of the assessment.
    /// </summary>
    public Guid AssessmentId { get; set; }

    /// <summary>
    /// The organisation this assessment belongs to.
    /// </summary>
    public Guid OrganizationId { get; set; }

    /// <summary>
    /// Current lifecycle status of the assessment.
    /// </summary>
    public AssessmentStatus Status { get; set; }

    /// <summary>
    /// Timestamp when the assessment was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when the assessment was started; null if not yet started.
    /// </summary>
    public DateTime? StartedAt { get; set; }

    /// <summary>
    /// Timestamp when the assessment was completed; null if not yet completed.
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Overall readiness level; populated only for completed assessments.
    /// </summary>
    public ReadinessLevel? ReadinessLevel { get; set; }

    /// <summary>
    /// Overall readiness score (0-100); populated only for completed assessments.
    /// </summary>
    public double? OverallScore { get; set; }

    /// <summary>
    /// Total number of assessment categories (always 6 for AI readiness).
    /// </summary>
    public int TotalCategories { get; set; }

    /// <summary>
    /// Number of categories that have been fully assessed (maturity level set and observations recorded).
    /// </summary>
    public int CompletedCategories { get; set; }

    /// <summary>
    /// Percentage of categories completed (0-100).
    /// </summary>
    public int CompletionPercentage { get; set; }
}
