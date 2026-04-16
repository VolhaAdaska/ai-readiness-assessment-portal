using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;

namespace AiReadinessAssessment.Application.InitialAssessment.Commands;
/// <summary>
/// Command to complete an initial assessment.
/// Validates all categories are complete, calculates scores, and generates recommendations.
/// </summary>
public class CompleteInitialAssessmentCommand : ICommand<CompleteAssessmentResponse>
{
    /// <summary>
    /// Gets the ID of the assessment to complete.
    /// </summary>
    public Guid AssessmentId { get; }

    /// <summary>
    /// Initializes a new instance of the CompleteInitialAssessmentCommand class.
    /// </summary>
    /// <param name="assessmentId">The assessment ID.</param>
    public CompleteInitialAssessmentCommand(Guid assessmentId)
    {
        AssessmentId = assessmentId;
    }
}