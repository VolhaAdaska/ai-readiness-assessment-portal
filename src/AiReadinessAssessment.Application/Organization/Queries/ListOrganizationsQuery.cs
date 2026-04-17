using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.Organization.Dtos.Responses;

namespace AiReadinessAssessment.Application.Organization.Queries;

/// <summary>
/// Query to retrieve all organizations for listing.
/// </summary>
public class ListOrganizationsQuery : IQuery<List<OrganizationResponse>>
{
}
