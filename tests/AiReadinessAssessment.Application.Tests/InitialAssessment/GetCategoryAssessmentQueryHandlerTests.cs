using AiReadinessAssessment.Application.Common.Exceptions;
using AiReadinessAssessment.Application.InitialAssessment.Queries;
using AiReadinessAssessment.Application.InitialAssessment.Queries.Handlers;
using AiReadinessAssessment.Application.Tests.Common;
using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.Tests.InitialAssessment;

public class GetCategoryAssessmentQueryHandlerTests
{
    private readonly FakeAssessmentRepository _repository = new();
    private readonly GetCategoryAssessmentQueryHandler _handler;

    public GetCategoryAssessmentQueryHandlerTests()
    {
        _handler = new GetCategoryAssessmentQueryHandler(_repository);
    }

    private BaselineAssessment CreateStartedAssessment()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        assessment.Start();
        _repository.Seed(assessment);
        return assessment;
    }

    [Fact]
    public async Task Handle_WithExistingCategory_ReturnsMappedResponse()
    {
        var assessment = CreateStartedAssessment();
        var category = assessment.GetCategoryAssessment(CategoryType.Technology)!;
        category.SetMaturityLevel(3);
        category.SetObservations("Some tooling in place.");

        var response = await _handler.Handle(new GetCategoryAssessmentQuery(assessment.Id, CategoryType.Technology));

        response.Category.Should().Be(CategoryType.Technology);
        response.MaturityLevel.Should().Be(3);
        response.Observations.Should().Be("Some tooling in place.");
        response.CategoryScore.Should().BeGreaterThan(0);
        response.IsComplete.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_WithUnassessedCategory_ReturnsIsCompleteFalse()
    {
        var assessment = CreateStartedAssessment();

        var response = await _handler.Handle(new GetCategoryAssessmentQuery(assessment.Id, CategoryType.Security));

        response.IsComplete.Should().BeFalse();
        response.MaturityLevel.Should().Be(0);
    }

    [Fact]
    public async Task Handle_WithUnknownAssessmentId_ThrowsNotFoundException()
    {
        var act = () => _handler.Handle(new GetCategoryAssessmentQuery(Guid.NewGuid(), CategoryType.Data));

        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_WhenCategoryNotYetInitialized_ThrowsNotFoundException()
    {
        // Assessment exists but has not been started — no categories present
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        _repository.Seed(assessment);

        var act = () => _handler.Handle(new GetCategoryAssessmentQuery(assessment.Id, CategoryType.Data));

        await act.Should().ThrowAsync<NotFoundException>();
    }
}
