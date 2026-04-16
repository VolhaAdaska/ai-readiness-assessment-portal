namespace AiReadinessAssessment.Domain.InitialAssessment;

/// <summary>
/// Represents the priority/urgency level of a recommendation.
/// </summary>
public enum Priority
{
    /// <summary>
    /// Must address immediately; blocks AI adoption.
    /// </summary>
    Critical,

    /// <summary>
    /// Should address soon; significant impact on readiness.
    /// </summary>
    High,

    /// <summary>
    /// Should address; moderate impact on readiness.
    /// </summary>
    Medium,

    /// <summary>
    /// Nice to address; minor impact on readiness.
    /// </summary>
    Low
}