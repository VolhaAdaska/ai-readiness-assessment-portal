using AiReadinessAssessment.Domain.Organization;

namespace AiReadinessAssessment.Application.Organization.Services;

/// <summary>
/// Repository contract for persisting and retrieving organizations.
/// </summary>
public interface IOrganizationRepository
{
    /// <summary>
    /// Adds a new organization to the repository.
    /// </summary>
    Task AddAsync(Domain.Organization.Organization organization, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all organizations.
    /// </summary>
    Task<List<Domain.Organization.Organization>> GetAllAsync(CancellationToken cancellationToken = default);
}
