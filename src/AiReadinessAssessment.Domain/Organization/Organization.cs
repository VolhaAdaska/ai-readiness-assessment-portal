namespace AiReadinessAssessment.Domain.Organization;

/// <summary>
/// Represents an organization being assessed for AI readiness.
/// </summary>
public class Organization
{
    /// <summary>
    /// Unique identifier for this organization.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Display name of the organization.
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// Timestamp when the organization was registered.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    private Organization()
    {
        // EF Core requires parameterless constructor
    }

    /// <summary>
    /// Creates a new organization.
    /// </summary>
    /// <param name="name">The organization name.</param>
    /// <returns>A new Organization.</returns>
    /// <exception cref="ArgumentException">Thrown if name is null or empty.</exception>
    public static Organization Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Organization name cannot be empty.", nameof(name));

        return new Organization
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            CreatedAt = DateTime.UtcNow
        };
    }
}
