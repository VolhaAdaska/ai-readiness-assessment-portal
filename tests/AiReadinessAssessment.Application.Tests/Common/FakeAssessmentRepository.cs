using AiReadinessAssessment.Application.InitialAssessment.Services;
using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.Tests.Common;

/// <summary>
/// In-memory fake for IInitialAssessmentRepository.
/// Avoids EF Core and keeps tests fast and deterministic.
/// </summary>
public class FakeAssessmentRepository : IInitialAssessmentRepository
{
    private readonly List<BaselineAssessment> _store = new();

    public BaselineAssessment? LastAdded => _store.LastOrDefault();
    public int UpdateCallCount { get; private set; }

    public Task AddAsync(BaselineAssessment assessment, CancellationToken cancellationToken = default)
    {
        _store.Add(assessment);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(BaselineAssessment assessment, CancellationToken cancellationToken = default)
    {
        UpdateCallCount++;
        return Task.CompletedTask;
    }

    public Task<BaselineAssessment?> GetByIdAsync(Guid assessmentId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store.FirstOrDefault(a => a.Id == assessmentId));
    }

    public Task<bool> ExistsAsync(Guid assessmentId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store.Any(a => a.Id == assessmentId));
    }

    public Task<List<BaselineAssessment>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store.ToList());
    }

    /// <summary>
    /// Seeds an assessment directly into the store for test setup.
    /// </summary>
    public void Seed(BaselineAssessment assessment) => _store.Add(assessment);
}
