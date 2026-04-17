using AiReadinessAssessment.Application.Organization.Services;

namespace AiReadinessAssessment.Application.Tests.Common;

/// <summary>
/// In-memory fake for IOrganizationRepository.
/// </summary>
public class FakeOrganizationRepository : IOrganizationRepository
{
    private readonly List<Domain.Organization.Organization> _store = new();

    public Task AddAsync(Domain.Organization.Organization organization, CancellationToken cancellationToken = default)
    {
        _store.Add(organization);
        return Task.CompletedTask;
    }

    public Task<List<Domain.Organization.Organization>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store.ToList());
    }

    /// <summary>
    /// Seeds an organization directly into the store for test setup.
    /// </summary>
    public void Seed(Domain.Organization.Organization organization) => _store.Add(organization);
}
