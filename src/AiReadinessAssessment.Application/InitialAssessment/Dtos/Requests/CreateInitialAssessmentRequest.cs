namespace AiReadinessAssessment.Application.InitialAssessment.Dtos.Requests;

/// <summary>
/// Request model for creating a new initial assessment.
/// </summary>
public class CreateInitialAssessmentRequest
{
    /// <summary>
    /// Gets or sets the organization ID for which to create the assessment.
    /// </summary>
    public Guid OrganizationId { get; set; }
}