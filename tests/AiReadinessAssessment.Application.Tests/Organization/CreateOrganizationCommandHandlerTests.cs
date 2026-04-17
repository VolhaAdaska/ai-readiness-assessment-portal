using AiReadinessAssessment.Application.Organization.Commands;
using AiReadinessAssessment.Application.Organization.Commands.Handlers;
using AiReadinessAssessment.Application.Tests.Common;

namespace AiReadinessAssessment.Application.Tests.Organization;

public class CreateOrganizationCommandHandlerTests
{
    private readonly FakeOrganizationRepository _repository = new();
    private readonly CreateOrganizationCommandHandler _handler;

    public CreateOrganizationCommandHandlerTests()
    {
        _handler = new CreateOrganizationCommandHandler(_repository);
    }

    [Fact]
    public async Task Handle_WithValidName_ReturnsOrganizationWithCorrectFields()
    {
        var command = new CreateOrganizationCommand("Acme Corporation");

        var response = await _handler.Handle(command);

        response.Id.Should().NotBeEmpty();
        response.Name.Should().Be("Acme Corporation");
        response.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
    }

    [Fact]
    public async Task Handle_WithValidName_PersistsOrganization()
    {
        var command = new CreateOrganizationCommand("Test Org");

        var response = await _handler.Handle(command);

        var stored = await _repository.GetAllAsync();
        stored.Should().ContainSingle()
            .Which.Id.Should().Be(response.Id);
    }

    [Fact]
    public async Task Handle_WithEmptyName_ThrowsArgumentException()
    {
        var command = new CreateOrganizationCommand(string.Empty);

        var act = () => _handler.Handle(command);

        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task Handle_WithWhitespaceName_ThrowsArgumentException()
    {
        var command = new CreateOrganizationCommand("   ");

        var act = () => _handler.Handle(command);

        await act.Should().ThrowAsync<ArgumentException>();
    }
}
