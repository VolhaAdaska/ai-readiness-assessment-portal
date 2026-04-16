namespace AiReadinessAssessment.Domain.InitialAssessment;

/// <summary>
/// Represents the lifecycle state of an initial assessment.
/// </summary>
public enum AssessmentStatus
{
    /// <summary>
    /// Assessment has been created but not yet started.
    /// </summary>
    NotStarted,

    /// <summary>
    /// Assessment is actively being conducted.
    /// </summary>
    InProgress,

    /// <summary>
    /// Assessment data collection is complete.
    /// </summary>
    Completed,

    /// <summary>
    /// Assessment has been closed and archived.
    /// </summary>
    Archived
}