namespace AiReadinessAssessment.Application.InitialAssessment.Dtos.Requests;

/// <summary>
/// Request model for starting an initial assessment.
/// </summary>
public class StartInitialAssessmentRequest
{
    /// <summary>
    /// Gets or sets the assessment ID to start.
    /// </summary>
    public Guid AssessmentId { get; set; }
}