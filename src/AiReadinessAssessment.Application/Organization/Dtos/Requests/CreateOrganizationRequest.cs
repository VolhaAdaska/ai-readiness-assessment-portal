namespace AiReadinessAssessment.Application.Organization.Dtos.Requests;

/// <summary>
/// Request DTO for creating an organization.
/// </summary>
public class CreateOrganizationRequest
{
    /// <summary>
    /// Gets or sets the organization name.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
