using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.InitialAssessment.Dtos.Requests;

/// <summary>
/// Request model for updating a category assessment.
/// Can update maturity level, observations, or both.
/// </summary>
public class UpdateCategoryAssessmentRequest
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
    /// Gets or sets the maturity level (1-5). Null if not being updated.
    /// </summary>
    public int? MaturityLevel { get; set; }

    /// <summary>
    /// Gets or sets the observations. Null if not being updated.
    /// </summary>
    public string? Observations { get; set; }
}