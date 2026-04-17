namespace AiReadinessAssessment.Application.Organization.Dtos.Responses;

/// <summary>
/// Response DTO for an organization.
/// </summary>
public class OrganizationResponse
{
    /// <summary>
    /// Gets or sets the organization ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the organization name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
