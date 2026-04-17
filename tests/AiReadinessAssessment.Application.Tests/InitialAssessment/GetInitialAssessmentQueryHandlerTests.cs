using AiReadinessAssessment.Application.Common.Exceptions;
using AiReadinessAssessment.Application.InitialAssessment.Queries;
using AiReadinessAssessment.Application.InitialAssessment.Queries.Handlers;
using AiReadinessAssessment.Application.Tests.Common;
using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.Tests.InitialAssessment;

public class GetInitialAssessmentQueryHandlerTests
{
    private readonly FakeAssessmentRepository _repository = new();
    private readonly GetInitialAssessmentQueryHandler _handler;

    public GetInitialAssessmentQueryHandlerTests()
    {
        _handler = new GetInitialAssessmentQueryHandler(_repository);
    }

    private BaselineAssessment CreateStartedAssessment()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        assessment.Start();
        _repository.Seed(assessment);
        return assessment;
    }

    [Fact]
    public async Task Handle_WithExistingAssessment_ReturnsMappedResponse()
    {
        var assessment = CreateStartedAssessment();

        var response = await _handler.Handle(new GetInitialAssessmentQuery(assessment.Id));

        response.Id.Should().Be(assessment.Id);
        response.OrganizationId.Should().Be(assessment.OrganizationId);
        response.Status.Should().Be(assessment.Status);
        response.CreatedAt.Should().Be(assessment.CreatedAt);
        response.StartedAt.Should().Be(assessment.StartedAt);
    }

    [Fact]
    public async Task Handle_WithStartedAssessment_ReturnsSixCategoryAssessments()
    {
        var assessment = CreateStartedAssessment();

        var response = await _handler.Handle(new GetInitialAssessmentQuery(assessment.Id));

        response.Categories.Should().HaveCount(6);
    }

    [Fact]
    public async Task Handle_CategoryResponse_ContainsCorrectFields()
    {
        var assessment = CreateStartedAssessment();
        var dataCategory = assessment.GetCategoryAssessment(CategoryType.Data)!;
        dataCategory.SetMaturityLevel(4);
        dataCategory.SetObservations("Data is well governed.");

        var response = await _handler.Handle(new GetInitialAssessmentQuery(assessment.Id));

        var dataCategoryResponse = response.Categories.Single(c => c.Category == CategoryType.Data);
        dataCategoryResponse.MaturityLevel.Should().Be(4);
        dataCategoryResponse.Observations.Should().Be("Data is well governed.");
        dataCategoryResponse.CategoryScore.Should().BeGreaterThan(0);
        dataCategoryResponse.IsComplete.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_WithCompletedAssessment_ReturnsReadinessLevel()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        assessment.Start();
        foreach (var c in assessment.CategoryAssessments)
        {
            c.SetMaturityLevel(5);
            c.SetObservations("Excellent");
        }
        assessment.Complete();
        _repository.Seed(assessment);

        var response = await _handler.Handle(new GetInitialAssessmentQuery(assessment.Id));

        response.ReadinessLevel.Should().NotBeNull();
        response.OverallScore.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Handle_WithInProgressAssessment_ReadinessLevelIsNull()
    {
        var assessment = CreateStartedAssessment();

        var response = await _handler.Handle(new GetInitialAssessmentQuery(assessment.Id));

        response.ReadinessLevel.Should().BeNull();
    }

    [Fact]
    public async Task Handle_WithUnknownAssessmentId_ThrowsNotFoundException()
    {
        var act = () => _handler.Handle(new GetInitialAssessmentQuery(Guid.NewGuid()));

        await act.Should().ThrowAsync<NotFoundException>();
    }
}
