using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;


/// <summary>
/// Response model for a recommendation.
/// </summary>
public class RecommendationResponse
{
    /// <summary>
    /// Gets or sets the recommendation ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the category this recommendation addresses.
    /// </summary>
    public CategoryType Category { get; set; }

    /// <summary>
    /// Gets or sets the priority level of the recommendation.
    /// </summary>
    public Priority Priority { get; set; }

    /// <summary>
    /// Gets or sets the recommendation title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the detailed description of the recommendation.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp when the recommendation was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}