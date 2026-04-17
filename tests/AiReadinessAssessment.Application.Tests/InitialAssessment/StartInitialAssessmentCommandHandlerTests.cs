using AiReadinessAssessment.Application.Common.Exceptions;
using AiReadinessAssessment.Application.InitialAssessment.Commands;
using AiReadinessAssessment.Application.InitialAssessment.Commands.Handlers;
using AiReadinessAssessment.Application.Tests.Common;
using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.Tests.InitialAssessment;

public class StartInitialAssessmentCommandHandlerTests
{
    private readonly FakeAssessmentRepository _repository = new();
    private readonly StartInitialAssessmentCommandHandler _handler;

    public StartInitialAssessmentCommandHandlerTests()
    {
        _handler = new StartInitialAssessmentCommandHandler(_repository);
    }

    [Fact]
    public async Task Handle_WithExistingNotStartedAssessment_TransitionsToInProgressAndInitializesCategories()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        _repository.Seed(assessment);

        var response = await _handler.Handle(new StartInitialAssessmentCommand(assessment.Id));

        response.AssessmentId.Should().Be(assessment.Id);
        response.Status.Should().Be(AssessmentStatus.InProgress);
        response.StartedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
        response.InitializedCategories.Should().HaveCount(6)
            .And.BeEquivalentTo(Enum.GetValues<CategoryType>());
    }

    [Fact]
    public async Task Handle_WithExistingNotStartedAssessment_PersistsUpdate()
    {
        var assessment = BaselineAssessment.Create(Guid.NewGuid());
        _repository.Seed(assessment);

        await _handler.Handle(new StartInitialAssessmentCommand(assessment.Id));

        _repository.UpdateCallCount.Should().Be(1);
    }

    [Fact]
    public async Task Handle_WithUnknownAssessmentId_ThrowsNotFoundException()
    {
        var command = new StartInitialAssessmentCommand(Guid.NewGuid());

        var act = () => _handler.Handle(command);

        await act.Should().ThrowAsync<NotFoundException>();
    }
}
