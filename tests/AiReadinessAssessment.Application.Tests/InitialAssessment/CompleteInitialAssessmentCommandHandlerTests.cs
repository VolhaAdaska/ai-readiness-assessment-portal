using AiReadinessAssessment.Application.Common.Exceptions;
using AiReadinessAssessment.Application.InitialAssessment.Commands;
using AiReadinessAssessment.Application.InitialAssessment.Commands.Handlers;
using AiReadinessAssessment.Application.Tests.Common;
using AiReadinessAssessment.Domain.InitialAssessment;
using AiReadinessAssessment.Application.InitialAssessment.Services;

namespace AiReadinessAssessment.Application.Tests.InitialAssessment;

public class CompleteInitialAssessmentCommandHandlerTests
{
    private readonly FakeAssessmentRepository _repository = new();

    private CompleteInitialAssessmentCommandHandler BuildHandler(FakeRecommendationGenerator? generator = null)
        => new(_repository, generator ?? new FakeRecommendationGenerator());

    private BaselineAssessment CreateFullyAssessedAssessment()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        assessment.Start();

        foreach (var category in assessment.CategoryAssessments)
        {
            category.SetMaturityLevel(3);
            category.SetObservations("Assessed");
        }

        _repository.Seed(assessment);
        return assessment;
    }

    [Fact]
    public async Task Handle_WithAllCategoriesComplete_TransitionsToCompletedStatus()
    {
        var assessment = CreateFullyAssessedAssessment();

        var response = await BuildHandler().Handle(new CompleteInitialAssessmentCommand(assessment.Id));

        response.Status.Should().Be(AssessmentStatus.Completed);
        response.AssessmentId.Should().Be(assessment.Id);
        response.CompletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
    }

    [Fact]
    public async Task Handle_CallsCompleteBeforeGeneratingRecommendations()
    {
        // The recommendation generator records the assessment status when called.
        // If Complete() is called first, the status seen by the generator will be Completed.
        AssessmentStatus? statusSeenByGenerator = null;

        var capturingGenerator = new CapturingRecommendationGenerator(a =>
        {
            statusSeenByGenerator = a.Status;
            return new List<Recommendation>();
        });

        var assessment = CreateFullyAssessedAssessment();

        await new CompleteInitialAssessmentCommandHandler(_repository, capturingGenerator)
            .Handle(new CompleteInitialAssessmentCommand(assessment.Id));

        statusSeenByGenerator.Should().Be(AssessmentStatus.Completed);
    }

    [Fact]
    public async Task Handle_RecommendationsGeneratedAreAttachedToAssessment()
    {
        var recommendation = Recommendation.Create(CategoryType.Data, Priority.High, "Title", "Description");
        var generator = new FakeRecommendationGenerator(new[] { recommendation });

        var assessment = CreateFullyAssessedAssessment();

        var response = await BuildHandler(generator).Handle(new CompleteInitialAssessmentCommand(assessment.Id));

        response.RecommendationCount.Should().Be(1);
        assessment.Recommendations.Should().ContainSingle();
    }

    [Fact]
    public async Task Handle_WithNoRecommendationsGenerated_CompletesSuccessfully()
    {
        var generator = new FakeRecommendationGenerator(); // returns empty list
        var assessment = CreateFullyAssessedAssessment();

        var response = await BuildHandler(generator).Handle(new CompleteInitialAssessmentCommand(assessment.Id));

        response.Status.Should().Be(AssessmentStatus.Completed);
        response.RecommendationCount.Should().Be(0);
    }

    [Fact]
    public async Task Handle_ReturnsOverallScoreAndReadinessLevel()
    {
        var assessment = CreateFullyAssessedAssessment();

        var response = await BuildHandler().Handle(new CompleteInitialAssessmentCommand(assessment.Id));

        response.OverallScore.Should().BeGreaterThan(0);
        response.CategoryScores.Should().HaveCount(6);
    }

    [Fact]
    public async Task Handle_PersistsAssessmentAfterCompletion()
    {
        var assessment = CreateFullyAssessedAssessment();

        await BuildHandler().Handle(new CompleteInitialAssessmentCommand(assessment.Id));

        _repository.UpdateCallCount.Should().Be(1);
    }

    [Fact]
    public async Task Handle_WithUnknownAssessmentId_ThrowsNotFoundException()
    {
        var act = () => BuildHandler().Handle(new CompleteInitialAssessmentCommand(Guid.NewGuid()));

        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_WithIncompleteCategoriesAssessment_ThrowsInvalidOperationException()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        assessment.Start();
        // Leave all categories at maturity 0 — incomplete
        _repository.Seed(assessment);

        var act = () => BuildHandler().Handle(new CompleteInitialAssessmentCommand(assessment.Id));

        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    /// <summary>
    /// Capturing fake that records the assessment state when the generator is called.
    /// </summary>
    private sealed class CapturingRecommendationGenerator : IRecommendationGenerator
    {
        private readonly Func<Domain.InitialAssessment.BaselineAssessment, List<Recommendation>> _capture;

        public CapturingRecommendationGenerator(Func<Domain.InitialAssessment.BaselineAssessment, List<Recommendation>> capture)
            => _capture = capture;

        public Task<List<Recommendation>> GenerateRecommendationsAsync(
            Domain.InitialAssessment.BaselineAssessment assessment,
            CancellationToken cancellationToken = default)
            => Task.FromResult(_capture(assessment));
    }
}
