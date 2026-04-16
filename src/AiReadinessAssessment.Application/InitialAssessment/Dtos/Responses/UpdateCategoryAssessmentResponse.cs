using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;

/// <summary>
/// Response model for updating a category assessment.
/// </summary>
public class UpdateCategoryAssessmentResponse
{
    /// <summary>
    /// Gets or sets the assessment ID.
    /// </summary>
    public Guid AssessmentId { get; set; }

    /// <summary>
    /// Gets or sets the category type.
    /// </summary>
    public CategoryType Category { get; set; }

    /// <summary>
    /// Gets or sets the updated maturity level.
    /// </summary>
    public int MaturityLevel { get; set; }

    /// <summary>
    /// Gets or sets the updated observations.
    /// </summary>
    public string Observations { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the calculated category score (0-100).
    /// </summary>
    public double CategoryScore { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when category was updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}