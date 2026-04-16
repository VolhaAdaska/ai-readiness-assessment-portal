using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.InitialAssessment.Services;

/// <summary>
/// Repository interface for persisting and retrieving initial assessments.
/// Implementation provided in Infrastructure layer.
/// </summary>
public interface IInitialAssessmentRepository
{
    /// <summary>
    /// Adds a new assessment to the repository.
    /// </summary>
    /// <param name="assessment">The assessment to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddAsync(AiReadinessAssessment.Domain.InitialAssessment.InitialAssessment assessment, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing assessment in the repository.
    /// </summary>
    /// <param name="assessment">The assessment to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateAsync(AiReadinessAssessment.Domain.InitialAssessment.InitialAssessment assessment, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an assessment by ID.
    /// </summary>
    /// <param name="assessmentId">The assessment ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The assessment if found; null otherwise.</returns>
    Task<AiReadinessAssessment.Domain.InitialAssessment.InitialAssessment?> GetByIdAsync(Guid assessmentId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if an assessment exists.
    /// </summary>
    /// <param name="assessmentId">The assessment ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if assessment exists; otherwise false.</returns>
    Task<bool> ExistsAsync(Guid assessmentId, CancellationToken cancellationToken = default);
}