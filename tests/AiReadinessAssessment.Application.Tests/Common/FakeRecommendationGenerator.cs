using AiReadinessAssessment.Application.InitialAssessment.Services;
using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.Tests.Common;

/// <summary>
/// Configurable fake for IRecommendationGenerator.
/// Returns a fixed list of recommendations by default; list can be overridden per test.
/// </summary>
public class FakeRecommendationGenerator : IRecommendationGenerator
{
    private readonly List<Recommendation> _recommendations;
    public int CallCount { get; private set; }

    public FakeRecommendationGenerator(IEnumerable<Recommendation>? recommendations = null)
    {
        _recommendations = recommendations?.ToList() ?? new List<Recommendation>();
    }

    public Task<List<Recommendation>> GenerateRecommendationsAsync(
        BaselineAssessment assessment,
        CancellationToken cancellationToken = default)
    {
        CallCount++;
        return Task.FromResult(_recommendations);
    }
}
