namespace AiReadinessAssessment.Application.InitialAssessment.Dtos.Requests;

/// <summary>
/// Request model for retrieving an initial assessment.
/// </summary>
public class GetInitialAssessmentRequest
{
    /// <summary>
    /// Gets or sets the assessment ID to retrieve.
    /// </summary>
    public Guid AssessmentId { get; set; }
}