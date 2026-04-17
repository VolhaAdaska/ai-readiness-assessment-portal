using AiReadinessAssessment.Application.InitialAssessment.Commands;
using AiReadinessAssessment.Application.InitialAssessment.Commands.Handlers;
using AiReadinessAssessment.Application.Tests.Common;
using AiReadinessAssessment.Domain.InitialAssessment;

namespace AiReadinessAssessment.Application.Tests.InitialAssessment;

public class CreateInitialAssessmentCommandHandlerTests
{
    private readonly FakeAssessmentRepository _repository = new();
    private readonly CreateInitialAssessmentCommandHandler _handler;

    public CreateInitialAssessmentCommandHandlerTests()
    {
        _handler = new CreateInitialAssessmentCommandHandler(_repository);
    }

    [Fact]
    public async Task Handle_WithValidOrganizationId_PersistsAndReturnsAssessment()
    {
        var orgId = Guid.NewGuid();
        var command = new CreateInitialAssessmentCommand(orgId);

        var response = await _handler.Handle(command);

        response.AssessmentId.Should().NotBeEmpty();
        response.Status.Should().Be(AssessmentStatus.NotStarted);
        response.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));

        _repository.LastAdded.Should().NotBeNull();
        _repository.LastAdded!.OrganizationId.Should().Be(orgId);
    }

    [Fact]
    public async Task Handle_WithValidOrganizationId_AssessmentIdInResponseMatchesPersisted()
    {
        var command = new CreateInitialAssessmentCommand(Guid.NewGuid());

        var response = await _handler.Handle(command);

        _repository.LastAdded!.Id.Should().Be(response.AssessmentId);
    }
}
