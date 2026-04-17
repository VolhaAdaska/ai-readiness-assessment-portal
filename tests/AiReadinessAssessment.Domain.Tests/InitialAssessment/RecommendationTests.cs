using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Domain.Tests.InitialAssessment;

public class RecommendationTests
{
    // ── Create ────────────────────────────────────────────────────────────────

    [Fact]
    public void Create_WithValidData_ReturnsRecommendation()
    {
        var recommendation = Recommendation.Create(
            CategoryType.Data,
            Priority.High,
            "Establish data governance",
            "Define ownership and quality standards for all data assets.");

        recommendation.Id.Should().NotBeEmpty();
        recommendation.Category.Should().Be(CategoryType.Data);
        recommendation.Priority.Should().Be(Priority.High);
        recommendation.Title.Should().Be("Establish data governance");
        recommendation.Description.Should().Be("Define ownership and quality standards for all data assets.");
        recommendation.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithEmptyTitle_ThrowsArgumentException(string title)
    {
        var act = () => Recommendation.Create(CategoryType.Security, Priority.Critical, title, "Valid description.");

        act.Should().Throw<ArgumentException>()
            .WithParameterName("title");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithEmptyDescription_ThrowsArgumentException(string description)
    {
        var act = () => Recommendation.Create(CategoryType.Security, Priority.Critical, "Valid title", description);

        act.Should().Throw<ArgumentException>()
            .WithParameterName("description");
    }

    [Fact]
    public void Create_TrimsWhitespaceFromTitleAndDescription()
    {
        var recommendation = Recommendation.Create(
            CategoryType.Governance,
            Priority.Medium,
            "  Title with spaces  ",
            "  Description with spaces  ");

        recommendation.Title.Should().Be("Title with spaces");
        recommendation.Description.Should().Be("Description with spaces");
    }

    [Theory]
    [InlineData(CategoryType.Data, Priority.Critical)]
    [InlineData(CategoryType.Technology, Priority.High)]
    [InlineData(CategoryType.Process, Priority.Medium)]
    [InlineData(CategoryType.Security, Priority.Low)]
    [InlineData(CategoryType.Governance, Priority.High)]
    [InlineData(CategoryType.TeamCapabilities, Priority.Medium)]
    public void Create_AllCategoryAndPriorityCombinations_Succeed(CategoryType category, Priority priority)
    {
        var act = () => Recommendation.Create(category, priority, "Title", "Description");

        act.Should().NotThrow();
    }

    // ── UpdatePriority ────────────────────────────────────────────────────────

    [Fact]
    public void UpdatePriority_ChangesPriority()
    {
        var recommendation = Recommendation.Create(CategoryType.Data, Priority.Low, "Title", "Description");

        recommendation.UpdatePriority(Priority.Critical);

        recommendation.Priority.Should().Be(Priority.Critical);
    }

    [Theory]
    [InlineData(Priority.Critical)]
    [InlineData(Priority.High)]
    [InlineData(Priority.Medium)]
    [InlineData(Priority.Low)]
    public void UpdatePriority_ToAnyPriority_Succeeds(Priority priority)
    {
        var recommendation = Recommendation.Create(CategoryType.Data, Priority.Low, "Title", "Description");

        recommendation.UpdatePriority(priority);

        recommendation.Priority.Should().Be(priority);
    }
}
