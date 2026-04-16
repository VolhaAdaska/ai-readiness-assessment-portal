using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;
using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.InitialAssessment.Commands;

/// <summary>
/// Command to update a category assessment with maturity level and/or observations.
/// </summary>
public class UpdateCategoryAssessmentCommand : ICommand<UpdateCategoryAssessmentResponse>
{
    /// <summary>
    /// Gets the ID of the assessment.
    /// </summary>
    public Guid AssessmentId { get; }

    /// <summary>
    /// Gets the category being updated.
    /// </summary>
    public CategoryType Category { get; }

    /// <summary>
    /// Gets the maturity level (1-5). Null if not being updated.
    /// </summary>
    public int? MaturityLevel { get; }

    /// <summary>
    /// Gets the observations/notes. Null if not being updated.
    /// </summary>
    public string? Observations { get; }

    /// <summary>
    /// Initializes a new instance of the UpdateCategoryAssessmentCommand class.
    /// </summary>
    /// <param name="assessmentId">The assessment ID.</param>
    /// <param name="category">The category type.</param>
    /// <param name="maturityLevel">The maturity level (1-5), or null to skip update.</param>
    /// <param name="observations">The observations text, or null to skip update.</param>
    public UpdateCategoryAssessmentCommand(
        Guid assessmentId,
        CategoryType category,
        int? maturityLevel = null,
        string? observations = null)
    {
        AssessmentId = assessmentId;
        Category = category;
        MaturityLevel = maturityLevel;
        Observations = observations;
    }
}