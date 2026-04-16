using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;

/// <summary>
/// Response model for a single category assessment.
/// </summary>
public class CategoryAssessmentResponse
{
    /// <summary>
    /// Gets or sets the category assessment ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the category type.
    /// </summary>
    public CategoryType Category { get; set; }

    /// <summary>
    /// Gets or sets the maturity level (0-5).
    /// </summary>
    public int MaturityLevel { get; set; }

    /// <summary>
    /// Gets or sets the assessor observations.
    /// </summary>
    public string Observations { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the calculated category score (0-100).
    /// </summary>
    public double CategoryScore { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the category assessment is complete.
    /// </summary>
    public bool IsComplete { get; set; }
}