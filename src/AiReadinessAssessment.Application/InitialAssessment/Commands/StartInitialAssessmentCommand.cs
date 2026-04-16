using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;

namespace AiReadinessAssessment.Application.InitialAssessment.Commands;

/// <summary>
/// Command to start an initial assessment (transition from NotStarted to InProgress).
/// Initializes all six required category assessments.
/// </summary>
public class StartInitialAssessmentCommand : ICommand<StartAssessmentResponse>
{
    /// <summary>
    /// Gets the ID of the assessment to start.
    /// </summary>
    public Guid AssessmentId { get; }

    /// <summary>
    /// Initializes a new instance of the StartInitialAssessmentCommand class.
    /// </summary>
    /// <param name="assessmentId">The assessment ID.</param>
    public StartInitialAssessmentCommand(Guid assessmentId)
    {
        AssessmentId = assessmentId;
    }
}