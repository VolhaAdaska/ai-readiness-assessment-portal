using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;
using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.InitialAssessment.Queries;

/// <summary>
/// Query to retrieve a single category assessment with details.
/// </summary>
public class GetCategoryAssessmentQuery : IQuery<CategoryAssessmentResponse>
{
    /// <summary>
    /// Gets the ID of the assessment.
    /// </summary>
    public Guid AssessmentId { get; }

    /// <summary>
    /// Gets the category type to retrieve.
    /// </summary>
    public CategoryType Category { get; }

    /// <summary>
    /// Initializes a new instance of the GetCategoryAssessmentQuery class.
    /// </summary>
    /// <param name="assessmentId">The assessment ID.</param>
    /// <param name="category">The category type.</param>
    public GetCategoryAssessmentQuery(Guid assessmentId, CategoryType category)
    {
        AssessmentId = assessmentId;
        Category = category;
    }
}