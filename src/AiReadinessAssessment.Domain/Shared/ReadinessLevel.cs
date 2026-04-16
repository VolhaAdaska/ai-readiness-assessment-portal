namespace AiReadinessAssessment.Domain.Shared;

/// <summary>
/// Represents the classification of organizational readiness based on assessment scores.
/// </summary>
public enum ReadinessLevel
{
    /// <summary>
    /// Score 0-19%. Significant barriers; immediate action required.
    /// </summary>
    Critical,

    /// <summary>
    /// Score 20-39%. Major gaps; substantial improvement effort needed.
    /// </summary>
    Low,

    /// <summary>
    /// Score 40-59%. Some gaps; targeted improvements needed.
    /// </summary>
    Moderate,

    /// <summary>
    /// Score 60-79%. Ready for adoption with support and guidance.
    /// </summary>
    Good,

    /// <summary>
    /// Score 80-100%. Highly prepared for AI adoption.
    /// </summary>
    Excellent
}