using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.InitialAssessment.Dtos.Requests;

/// <summary>
/// Request model for retrieving a category assessment.
/// </summary>
public class GetCategoryAssessmentRequest
{
    /// <summary>
    /// Gets or sets the assessment ID.
    /// </summary>
    public Guid AssessmentId { get; set; }

    /// <summary>
    /// Gets or sets the category type to retrieve.
    /// </summary>
    public CategoryType Category { get; set; }
}