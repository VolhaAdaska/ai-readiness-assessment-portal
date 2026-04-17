using AiReadinessAssessment.Application.Organization.Services;
using AiReadinessAssessment.Domain.Organization;
using Microsoft.EntityFrameworkCore;

namespace AiReadinessAssessment.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for persisting and retrieving organizations.
/// </summary>
public class OrganizationRepository : IOrganizationRepository
{
    private readonly AiReadinessAssessmentDbContext _context;

    /// <summary>
    /// Initializes a new instance of the OrganizationRepository class.
    /// </summary>
    /// <param name="context">The DbContext instance.</param>
    public OrganizationRepository(AiReadinessAssessmentDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new organization to the repository.
    /// </summary>
    public async Task AddAsync(Organization organization, CancellationToken cancellationToken = default)
    {
        await _context.Organizations.AddAsync(organization, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves all organizations.
    /// </summary>
    public async Task<List<Organization>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Organizations.ToListAsync(cancellationToken);
    }
}
