using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;

/// <summary>
/// Response model for starting an initial assessment.
/// </summary>
public class StartAssessmentResponse
{
    /// <summary>
    /// Gets or sets the assessment ID.
    /// </summary>
    public Guid AssessmentId { get; set; }

    /// <summary>
    /// Gets or sets the assessment status (InProgress).
    /// </summary>
    public AssessmentStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when assessment was started.
    /// </summary>
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// Gets or sets the list of initialized categories.
    /// </summary>
    public List<CategoryType> InitializedCategories { get; set; } = new();
}