using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Domain.Tests.InitialAssessment;

public class CategoryAssessmentTests
{
    // ── Create ────────────────────────────────────────────────────────────────

    [Fact]
    public void Create_ReturnsNewInstanceWithZeroMaturityAndNoObservations()
    {
        var category = CategoryAssessment.Create(CategoryType.Technology);

        category.Category.Should().Be(CategoryType.Technology);
        category.MaturityLevel.Should().Be(0);
        category.Observations.Should().BeEmpty();
        category.Responses.Should().BeEmpty();
        category.Id.Should().NotBeEmpty();
    }

    // ── IsComplete ────────────────────────────────────────────────────────────

    [Fact]
    public void IsComplete_WhenMaturityIsZero_ReturnsFalse()
    {
        var category = CategoryAssessment.Create(CategoryType.Data);
        category.SetObservations("Some observations");

        category.IsComplete().Should().BeFalse();
    }

    [Fact]
    public void IsComplete_WhenObservationsAreEmpty_ReturnsFalse()
    {
        var category = CategoryAssessment.Create(CategoryType.Data);
        category.SetMaturityLevel(3);

        category.IsComplete().Should().BeFalse();
    }

    [Fact]
    public void IsComplete_WhenMaturityIsSetAndObservationsProvided_ReturnsTrue()
    {
        var category = CategoryAssessment.Create(CategoryType.Data);
        category.SetMaturityLevel(3);
        category.SetObservations("All data pipelines documented.");

        category.IsComplete().Should().BeTrue();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    public void IsComplete_WithAnyValidMaturityLevelAndObservations_ReturnsTrue(int level)
    {
        var category = CategoryAssessment.Create(CategoryType.Security);
        category.SetMaturityLevel(level);
        category.SetObservations("Assessed");

        category.IsComplete().Should().BeTrue();
    }

    // ── SetMaturityLevel ──────────────────────────────────────────────────────

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(5)]
    public void SetMaturityLevel_WithinValidRange_SetsLevel(int level)
    {
        var category = CategoryAssessment.Create(CategoryType.Governance);

        category.SetMaturityLevel(level);

        category.MaturityLevel.Should().Be(level);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(6)]
    public void SetMaturityLevel_OutsideValidRange_ThrowsArgumentOutOfRangeException(int level)
    {
        var category = CategoryAssessment.Create(CategoryType.Governance);

        var act = () => category.SetMaturityLevel(level);

        act.Should().Throw<ArgumentOutOfRangeException>()
            .WithParameterName("level");
    }

    // ── SetObservations ───────────────────────────────────────────────────────

    [Fact]
    public void SetObservations_WithNull_SetsEmptyString()
    {
        var category = CategoryAssessment.Create(CategoryType.Process);

        category.SetObservations(null);

        category.Observations.Should().BeEmpty();
    }

    [Fact]
    public void SetObservations_WithText_SetsValue()
    {
        var category = CategoryAssessment.Create(CategoryType.Process);

        category.SetObservations("Processes are partially documented.");

        category.Observations.Should().Be("Processes are partially documented.");
    }

    // ── RecordResponse ────────────────────────────────────────────────────────

    [Fact]
    public void RecordResponse_WithValidData_AddsResponse()
    {
        var category = CategoryAssessment.Create(CategoryType.TeamCapabilities);
        var questionId = Guid.NewGuid();

        category.RecordResponse(questionId, "Yes, we have training programmes.");

        category.Responses.Should().ContainSingle()
            .Which.QuestionId.Should().Be(questionId);
    }

    [Fact]
    public void RecordResponse_DuplicateQuestion_ThrowsInvalidOperationException()
    {
        var category = CategoryAssessment.Create(CategoryType.TeamCapabilities);
        var questionId = Guid.NewGuid();
        category.RecordResponse(questionId, "First answer");

        var act = () => category.RecordResponse(questionId, "Second answer");

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void RecordResponse_WithEmptyQuestionId_ThrowsArgumentException()
    {
        var category = CategoryAssessment.Create(CategoryType.TeamCapabilities);

        var act = () => category.RecordResponse(Guid.Empty, "Answer");

        act.Should().Throw<ArgumentException>()
            .WithParameterName("questionId");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void RecordResponse_WithEmptyAnswer_ThrowsArgumentException(string answer)
    {
        var category = CategoryAssessment.Create(CategoryType.TeamCapabilities);

        var act = () => category.RecordResponse(Guid.NewGuid(), answer);

        act.Should().Throw<ArgumentException>()
            .WithParameterName("answer");
    }

    [Fact]
    public void GetResponseCount_ReturnsNumberOfRecordedResponses()
    {
        var category = CategoryAssessment.Create(CategoryType.Data);
        category.RecordResponse(Guid.NewGuid(), "Answer 1");
        category.RecordResponse(Guid.NewGuid(), "Answer 2");

        category.GetResponseCount().Should().Be(2);
    }

    // ── CalculateCategoryScore ────────────────────────────────────────────────

    [Fact]
    public void CalculateCategoryScore_WithMaturityFiveAndNoResponses_Returns80()
    {
        var category = CategoryAssessment.Create(CategoryType.Data);
        category.SetMaturityLevel(5);

        category.CalculateCategoryScore().Should().Be(80);
    }

    [Fact]
    public void CalculateCategoryScore_WithZeroMaturityAndNoResponses_ReturnsZero()
    {
        var category = CategoryAssessment.Create(CategoryType.Data);

        category.CalculateCategoryScore().Should().Be(0);
    }

    [Fact]
    public void CalculateCategoryScore_ResponseBonusCapAt20()
    {
        // 8 responses × 2.5 = 20 (capped); maturity 5 = 80; total = 100
        var category = CategoryAssessment.Create(CategoryType.Technology);
        category.SetMaturityLevel(5);
        for (var i = 0; i < 8; i++)
            category.RecordResponse(Guid.NewGuid(), $"Answer {i}");

        category.CalculateCategoryScore().Should().Be(100);
    }

    [Fact]
    public void CalculateCategoryScore_ResponseBonusDoesNotExceed20()
    {
        // 10 responses still capped at 20
        var category = CategoryAssessment.Create(CategoryType.Technology);
        category.SetMaturityLevel(5);
        for (var i = 0; i < 10; i++)
            category.RecordResponse(Guid.NewGuid(), $"Answer {i}");

        category.CalculateCategoryScore().Should().Be(100);
    }

    [Fact]
    public void CalculateCategoryScore_IsRoundedToTwoDecimalPlaces()
    {
        var category = CategoryAssessment.Create(CategoryType.Security);
        category.SetMaturityLevel(3); // 3/5 * 80 = 48.0

        category.CalculateCategoryScore().Should().Be(48.0);
    }
}
