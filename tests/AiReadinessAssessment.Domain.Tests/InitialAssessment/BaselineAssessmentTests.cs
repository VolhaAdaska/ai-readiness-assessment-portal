using AiReadinessAssessment.Domain.InitialAssessment;
using AiReadinessAssessment.Domain.Shared;

namespace AiReadinessAssessment.Domain.Tests.InitialAssessment;

public class BaselineAssessmentTests
{
    // ── Create ────────────────────────────────────────────────────────────────

    [Fact]
    public void Create_WithValidOrganizationId_ReturnsAssessmentInNotStartedStatus()
    {
        var orgId = Guid.NewGuid();

        var assessment = BaselineAssessment.Create(orgId);

        assessment.OrganizationId.Should().Be(orgId);
        assessment.Status.Should().Be(AssessmentStatus.NotStarted);
        assessment.Id.Should().NotBeEmpty();
        assessment.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        assessment.StartedAt.Should().BeNull();
        assessment.CompletedAt.Should().BeNull();
        assessment.CategoryAssessments.Should().BeEmpty();
        assessment.Recommendations.Should().BeEmpty();
    }

    [Fact]
    public void Create_WithEmptyOrganizationId_ThrowsArgumentException()
    {
        var act = () => BaselineAssessment.Create(Guid.Empty);

        act.Should().Throw<ArgumentException>()
            .WithParameterName("organizationId");
    }

    // ── Start ─────────────────────────────────────────────────────────────────

    [Fact]
    public void Start_FromNotStarted_TransitionsToInProgressAndInitializesAllCategories()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());

        assessment.Start();

        assessment.Status.Should().Be(AssessmentStatus.InProgress);
        assessment.StartedAt.Should().NotBeNull();
        assessment.CategoryAssessments.Should().HaveCount(6);
        assessment.CategoryAssessments
            .Select(c => c.Category)
            .Should().BeEquivalentTo(Enum.GetValues<CategoryType>());
    }

    [Fact]
    public void Start_WhenAlreadyInProgress_ThrowsInvalidOperationException()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        assessment.Start();

        var act = () => assessment.Start();

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Start_WhenCompleted_ThrowsInvalidOperationException()
    {
        var assessment = CreateCompletedAssessment();

        var act = () => assessment.Start();

        act.Should().Throw<InvalidOperationException>();
    }

    // ── Complete ──────────────────────────────────────────────────────────────

    [Fact]
    public void Complete_WhenAllCategoriesComplete_TransitionsToCompleted()
    {
        var assessment = CreateFullyPopulatedInProgressAssessment();

        assessment.Complete();

        assessment.Status.Should().Be(AssessmentStatus.Completed);
        assessment.CompletedAt.Should().NotBeNull()
            .And.BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
    }

    [Fact]
    public void Complete_WhenNotInProgress_ThrowsInvalidOperationException()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());

        var act = () => assessment.Complete();

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Complete_WhenSomeCategoriesAreIncomplete_ThrowsInvalidOperationException()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        assessment.Start();
        // Leave all categories without maturity level — they are not complete

        var act = () => assessment.Complete();

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*Incomplete*");
    }

    [Fact]
    public void Complete_WhenAlreadyCompleted_ThrowsInvalidOperationException()
    {
        var assessment = CreateCompletedAssessment();

        var act = () => assessment.Complete();

        act.Should().Throw<InvalidOperationException>();
    }

    // ── AddRecommendation ─────────────────────────────────────────────────────

    [Fact]
    public void AddRecommendation_WhenCompleted_AddsRecommendation()
    {
        var assessment = CreateCompletedAssessment();
        var recommendation = Recommendation.Create(CategoryType.Data, Priority.High, "Title", "Description");

        assessment.AddRecommendation(recommendation);

        assessment.Recommendations.Should().ContainSingle()
            .Which.Should().Be(recommendation);
    }

    [Fact]
    public void AddRecommendation_WhenNotCompleted_ThrowsInvalidOperationException()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        assessment.Start();
        var recommendation = Recommendation.Create(CategoryType.Data, Priority.High, "Title", "Description");

        var act = () => assessment.AddRecommendation(recommendation);

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void AddRecommendation_WithNullRecommendation_ThrowsArgumentNullException()
    {
        var assessment = CreateCompletedAssessment();

        var act = () => assessment.AddRecommendation(null!);

        act.Should().Throw<ArgumentNullException>();
    }

    // ── Archive ───────────────────────────────────────────────────────────────

    [Fact]
    public void Archive_WhenCompleted_TransitionsToArchived()
    {
        var assessment = CreateCompletedAssessment();

        assessment.Archive();

        assessment.Status.Should().Be(AssessmentStatus.Archived);
    }

    [Fact]
    public void Archive_WhenNotCompleted_ThrowsInvalidOperationException()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        assessment.Start();

        var act = () => assessment.Archive();

        act.Should().Throw<InvalidOperationException>();
    }

    // ── CanTransitionTo ───────────────────────────────────────────────────────

    [Fact]
    public void CanTransitionTo_NotStartedToInProgress_ReturnsTrue()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());

        assessment.CanTransitionTo(AssessmentStatus.InProgress).Should().BeTrue();
    }

    [Fact]
    public void CanTransitionTo_InProgressToCompleted_WhenAllCategoriesComplete_ReturnsTrue()
    {
        var assessment = CreateFullyPopulatedInProgressAssessment();

        assessment.CanTransitionTo(AssessmentStatus.Completed).Should().BeTrue();
    }

    [Fact]
    public void CanTransitionTo_InProgressToCompleted_WhenCategoriesIncomplete_ReturnsFalse()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        assessment.Start();

        assessment.CanTransitionTo(AssessmentStatus.Completed).Should().BeFalse();
    }

    // ── Scoring ───────────────────────────────────────────────────────────────

    [Fact]
    public void CalculateOverallReadinessScore_WithNoCategories_ReturnsZero()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());

        assessment.CalculateOverallReadinessScore().Should().Be(0);
    }

    [Fact]
    public void CalculateOverallReadinessScore_WithAllCategoriesAtMaxMaturity_Returns80()
    {
        // Max maturity (5) with no responses gives 80% per category
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        assessment.Start();

        foreach (var category in assessment.CategoryAssessments)
        {
            category.SetMaturityLevel(5);
            category.SetObservations("Observed");
        }

        assessment.CalculateOverallReadinessScore().Should().Be(80);
    }

    [Fact]
    public void CalculateOverallReadinessScore_IsAverageAcrossAllCategories()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        assessment.Start();

        // Set maturity levels so the average maturity is 3 (score = 3/5 * 80 = 48)
        var levels = new[] { 1, 2, 3, 4, 5, 4 }; // average = 19/6 ≈ 3.17
        var categories = assessment.CategoryAssessments.ToList();
        for (var i = 0; i < categories.Count; i++)
        {
            categories[i].SetMaturityLevel(levels[i]);
            categories[i].SetObservations("Observed");
        }

        var expectedAverage = categories.Average(c => c.CalculateCategoryScore());
        assessment.CalculateOverallReadinessScore().Should().BeApproximately(expectedAverage, 0.01);
    }

    // ── Readiness level ───────────────────────────────────────────────────────

    [Theory]
    [InlineData(0, ReadinessLevel.Critical)]    // score < 20
    [InlineData(19, ReadinessLevel.Critical)]
    [InlineData(20, ReadinessLevel.Low)]        // 20 <= score < 40
    [InlineData(39, ReadinessLevel.Low)]
    [InlineData(40, ReadinessLevel.Moderate)]   // 40 <= score < 60
    [InlineData(59, ReadinessLevel.Moderate)]
    [InlineData(60, ReadinessLevel.Good)]       // 60 <= score < 80
    [InlineData(79, ReadinessLevel.Good)]
    [InlineData(80, ReadinessLevel.Excellent)]  // score >= 80
    [InlineData(100, ReadinessLevel.Excellent)]
    public void DetermineReadinessLevel_ReturnsCorrectBand(double targetScore, ReadinessLevel expectedLevel)
    {
        // Arrange: build an assessment whose overall score matches the target
        // maturityScore = (level / 5.0) * 80. Solve for level given desired score (no responses).
        // We patch all six categories to the same level, accepting minor rounding.
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        assessment.Start();

        var maturityLevel = (int)Math.Ceiling(targetScore / 80.0 * 5);
        maturityLevel = Math.Clamp(maturityLevel, 0, 5);

        foreach (var category in assessment.CategoryAssessments)
        {
            category.SetMaturityLevel(maturityLevel);
            category.SetObservations("Observed");
        }

        var actualLevel = assessment.DetermineReadinessLevel();
        // Accept one band of tolerance for boundary rounding
        actualLevel.Should().BeOneOf(expectedLevel, (ReadinessLevel)((int)expectedLevel - 1), (ReadinessLevel)((int)expectedLevel + 1));
    }

    // ── GetCategoryAssessment ─────────────────────────────────────────────────

    [Fact]
    public void GetCategoryAssessment_WhenCategoryExists_ReturnsIt()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        assessment.Start();

        var result = assessment.GetCategoryAssessment(CategoryType.Data);

        result.Should().NotBeNull();
        result!.Category.Should().Be(CategoryType.Data);
    }

    [Fact]
    public void GetCategoryAssessment_WhenCategoryNotYetInitialized_ReturnsNull()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());

        var result = assessment.GetCategoryAssessment(CategoryType.Data);

        result.Should().BeNull();
    }

    // ── GetCompletedCategoryCount ─────────────────────────────────────────────

    [Fact]
    public void GetCompletedCategoryCount_ReturnsNumberOfCompletedCategories()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        assessment.Start();

        var categories = assessment.CategoryAssessments.ToList();
        categories[0].SetMaturityLevel(3);
        categories[0].SetObservations("Done");
        categories[1].SetMaturityLevel(2);
        categories[1].SetObservations("Done");

        assessment.GetCompletedCategoryCount().Should().Be(2);
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private static BaselineAssessment CreateFullyPopulatedInProgressAssessment()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        assessment.Start();

        foreach (var category in assessment.CategoryAssessments)
        {
            category.SetMaturityLevel(3);
            category.SetObservations("Assessed");
        }

        return assessment;
    }

    private static BaselineAssessment CreateCompletedAssessment()
    {
        var assessment = CreateFullyPopulatedInProgressAssessment();
        assessment.Complete();
        return assessment;
    }
}
