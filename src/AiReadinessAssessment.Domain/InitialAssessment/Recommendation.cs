namespace AiReadinessAssessment.Domain.InitialAssessment;

/// <summary>
/// Represents an improvement recommendation generated from an assessment.
/// Owned by InitialAssessment; provides actionable next steps.
/// </summary>
public class Recommendation
{
    /// <summary>
    /// Unique identifier for this recommendation.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// The category this recommendation addresses.
    /// </summary>
    public CategoryType Category { get; private set; }

    /// <summary>
    /// Priority/urgency level of this recommendation.
    /// </summary>
    public Priority Priority { get; private set; }

    /// <summary>
    /// Short title of the recommendation.
    /// </summary>
    public string Title { get; private set; } = string.Empty;

    /// <summary>
    /// Detailed description of the recommendation and its rationale.
    /// </summary>
    public string Description { get; private set; } = string.Empty;

    /// <summary>
    /// Timestamp when this recommendation was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    private Recommendation()
    {
        // EF Core requires parameterless constructor
    }

    /// <summary>
    /// Creates a new recommendation for the specified category.
    /// </summary>
    /// <param name="category">The category this recommendation addresses.</param>
    /// <param name="priority">The priority level of this recommendation.</param>
    /// <param name="title">Short title of the recommendation.</param>
    /// <param name="description">Detailed description and rationale.</param>
    /// <returns>A new Recommendation.</returns>
    /// <exception cref="ArgumentException">Thrown if title or description are empty.</exception>
    public static Recommendation Create(
        CategoryType category,
        Priority priority,
        string title,
        string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));

        return new Recommendation
        {
            Id = Guid.NewGuid(),
            Category = category,
            Priority = priority,
            Title = title.Trim(),
            Description = description.Trim(),
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Updates the priority of this recommendation.
    /// </summary>
    /// <param name="newPriority">The new priority level.</param>
    public void UpdatePriority(Priority newPriority)
    {
        Priority = newPriority;
    }
}