using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;

/// <summary>
/// Response model for creating an initial assessment.
/// </summary>
public class CreateInitialAssessmentResponse
{
    /// <summary>
    /// Gets or sets the newly created assessment ID.
    /// </summary>
    public Guid AssessmentId { get; set; }

    /// <summary>
    /// Gets or sets the assessment status (NotStarted).
    /// </summary>
    public AssessmentStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}