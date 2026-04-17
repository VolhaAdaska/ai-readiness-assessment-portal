using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.Organization.Dtos.Responses;
using AiReadinessAssessment.Application.Organization.Services;

namespace AiReadinessAssessment.Application.Organization.Queries.Handlers;

/// <summary>
/// Handler for ListOrganizationsQuery.
/// Returns all organizations ordered by name.
/// </summary>
public class ListOrganizationsQueryHandler : IQueryHandler<ListOrganizationsQuery, List<OrganizationResponse>>
{
    private readonly IOrganizationRepository _repository;

    /// <summary>
    /// Initializes a new instance of the ListOrganizationsQueryHandler class.
    /// </summary>
    /// <param name="repository">The organization repository.</param>
    public ListOrganizationsQueryHandler(IOrganizationRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Handles the list organizations query.
    /// </summary>
    public async Task<List<OrganizationResponse>> Handle(
        ListOrganizationsQuery query,
        CancellationToken cancellationToken = default)
    {
        var organizations = await _repository.GetAllAsync(cancellationToken);

        return organizations
            .OrderBy(o => o.Name)
            .Select(o => new OrganizationResponse
            {
                Id = o.Id,
                Name = o.Name,
                CreatedAt = o.CreatedAt
            })
            .ToList();
    }
}
