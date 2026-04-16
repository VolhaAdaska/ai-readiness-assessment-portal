namespace AiReadinessAssessment.Domain.InitialAssessment;

/// <summary>
/// Represents the six dimensions of organizational readiness for AI adoption.
/// </summary>
public enum CategoryType
{
    /// <summary>
    /// Data infrastructure, quality, availability, and governance.
    /// </summary>
    Data,

    /// <summary>
    /// AI/ML infrastructure, tools, platforms, and technical capabilities.
    /// </summary>
    Technology,

    /// <summary>
    /// Process maturity, automation readiness, and workflow optimization.
    /// </summary>
    Process,

    /// <summary>
    /// Data security, compliance, and access controls.
    /// </summary>
    Security,

    /// <summary>
    /// Decision-making frameworks, accountability, and oversight structures.
    /// </summary>
    Governance,

    /// <summary>
    /// Skills, training, cultural readiness, and team composition.
    /// </summary>
    TeamCapabilities
}