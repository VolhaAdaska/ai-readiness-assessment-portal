using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;

namespace AiReadinessAssessment.Application.InitialAssessment.Commands;
/// <summary>
/// Command to create a new initial assessment for an organization.
/// </summary>
public class CreateInitialAssessmentCommand : ICommand<CreateInitialAssessmentResponse>
{
    /// <summary>
    /// Gets the ID of the organization for which the assessment is being created.
    /// </summary>
    public Guid OrganizationId { get; }

    /// <summary>
    /// Initializes a new instance of the CreateInitialAssessmentCommand class.
    /// </summary>
    /// <param name="organizationId">The organization ID.</param>
    public CreateInitialAssessmentCommand(Guid organizationId)
    {
        OrganizationId = organizationId;
    }
}