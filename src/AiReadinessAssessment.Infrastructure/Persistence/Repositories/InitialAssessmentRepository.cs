using AiReadinessAssessment.Application.InitialAssessment.Services;
using AiReadinessAssessment.Domain.InitialAssessment;
using Microsoft.EntityFrameworkCore;

namespace AiReadinessAssessment.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for persisting and retrieving initial assessments.
/// Implements IInitialAssessmentRepository contract from Application layer.
/// </summary>
public class InitialAssessmentRepository : IInitialAssessmentRepository
{
    private readonly AiReadinessAssessmentDbContext _context;

    /// <summary>
    /// Initializes a new instance of the InitialAssessmentRepository class.
    /// </summary>
    /// <param name="context">The DbContext instance.</param>
    public InitialAssessmentRepository(AiReadinessAssessmentDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new assessment to the repository.
    /// </summary>
    /// <param name="assessment">The assessment to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task AddAsync(InitialAssessment assessment, CancellationToken cancellationToken = default)
    {
        await _context.InitialAssessments.AddAsync(assessment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates an existing assessment in the repository.
    /// </summary>
    /// <param name="assessment">The assessment to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task UpdateAsync(InitialAssessment assessment, CancellationToken cancellationToken = default)
    {
        _context.InitialAssessments.Update(assessment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves an assessment by ID with all related entities (category assessments, responses, recommendations).
    /// </summary>
    /// <param name="assessmentId">The assessment ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The assessment if found; null otherwise.</returns>
    public async Task<InitialAssessment?> GetByIdAsync(Guid assessmentId, CancellationToken cancellationToken = default)
    {
        return await _context.InitialAssessments
            .Include(a => a.CategoryAssessments)
            .ThenInclude(c => c.Responses)
            .Include(a => a.Recommendations)
            .FirstOrDefaultAsync(a => a.Id == assessmentId, cancellationToken);
    }

    /// <summary>
    /// Checks if an assessment exists.
    /// </summary>
    /// <param name="assessmentId">The assessment ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if assessment exists; otherwise false.</returns>
    public async Task<bool> ExistsAsync(Guid assessmentId, CancellationToken cancellationToken = default)
    {
        return await _context.InitialAssessments
            .AnyAsync(a => a.Id == assessmentId, cancellationToken);
    }
}
