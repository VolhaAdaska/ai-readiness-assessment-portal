namespace AiReadinessAssessment.Application.InitialAssessment.Dtos.Requests;

/// <summary>
/// Request model for completing an initial assessment.
/// </summary>
public class CompleteAssessmentRequest
{
    /// <summary>
    /// Gets or sets the assessment ID to complete.
    /// </summary>
    public Guid AssessmentId { get; set; }
}