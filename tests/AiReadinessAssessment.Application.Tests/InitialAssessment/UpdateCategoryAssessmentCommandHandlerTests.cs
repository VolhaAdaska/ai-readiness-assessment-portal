using AiReadinessAssessment.Application.Common.Exceptions;
using AiReadinessAssessment.Application.InitialAssessment.Commands;
using AiReadinessAssessment.Application.InitialAssessment.Commands.Handlers;
using AiReadinessAssessment.Application.Tests.Common;
using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.Tests.InitialAssessment;

public class UpdateCategoryAssessmentCommandHandlerTests
{
    private readonly FakeAssessmentRepository _repository = new();
    private readonly UpdateCategoryAssessmentCommandHandler _handler;

    public UpdateCategoryAssessmentCommandHandlerTests()
    {
        _handler = new UpdateCategoryAssessmentCommandHandler(_repository);
    }

    private BaselineAssessment CreateStartedAssessment()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        assessment.Start();
        _repository.Seed(assessment);
        return assessment;
    }

    [Fact]
    public async Task Handle_WithValidUpdate_UpdatesMaturityLevelAndObservations()
    {
        var assessment = CreateStartedAssessment();
        var command = new UpdateCategoryAssessmentCommand(assessment.Id, CategoryType.Data, 4, "Data pipelines in place.");

        var response = await _handler.Handle(command);

        response.AssessmentId.Should().Be(assessment.Id);
        response.Category.Should().Be(CategoryType.Data);
        response.MaturityLevel.Should().Be(4);
        response.Observations.Should().Be("Data pipelines in place.");
        response.CategoryScore.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Handle_WithValidUpdate_PersistsUpdate()
    {
        var assessment = CreateStartedAssessment();
        var command = new UpdateCategoryAssessmentCommand(assessment.Id, CategoryType.Data, 3, "Observed.");

        await _handler.Handle(command);

        _repository.UpdateCallCount.Should().Be(1);
    }

    [Fact]
    public async Task Handle_WithNullMaturityLevel_DoesNotChangeMaturityLevel()
    {
        var assessment = CreateStartedAssessment();
        var dataCategory = assessment.GetCategoryAssessment(CategoryType.Data)!;
        dataCategory.SetMaturityLevel(3);

        var command = new UpdateCategoryAssessmentCommand(assessment.Id, CategoryType.Data, null, "Updated observations only.");

        var response = await _handler.Handle(command);

        response.MaturityLevel.Should().Be(3);
        response.Observations.Should().Be("Updated observations only.");
    }

    [Fact]
    public async Task Handle_WithUnknownAssessmentId_ThrowsNotFoundException()
    {
        var command = new UpdateCategoryAssessmentCommand(Guid.NewGuid(), CategoryType.Data, 3, "Obs");

        var act = () => _handler.Handle(command);

        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_WithAssessmentNotYetStarted_ThrowsNotFoundException()
    {
        // Assessment exists but categories not initialized — category won't be found
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        _repository.Seed(assessment);

        var command = new UpdateCategoryAssessmentCommand(assessment.Id, CategoryType.Data, 3, "Obs");

        var act = () => _handler.Handle(command);

        await act.Should().ThrowAsync<NotFoundException>();
    }
}
