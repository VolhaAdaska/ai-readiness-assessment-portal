using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.Organization.Dtos.Responses;

namespace AiReadinessAssessment.Application.Organization.Commands;

/// <summary>
/// Command to create a new organization.
/// </summary>
public class CreateOrganizationCommand : ICommand<OrganizationResponse>
{
    /// <summary>
    /// Gets the organization name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Initializes a new instance of the CreateOrganizationCommand class.
    /// </summary>
    /// <param name="name">The organization name.</param>
    public CreateOrganizationCommand(string name)
    {
        Name = name;
    }
}
