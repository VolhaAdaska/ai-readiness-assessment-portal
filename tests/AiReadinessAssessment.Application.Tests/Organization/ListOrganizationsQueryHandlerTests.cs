using AiReadinessAssessment.Application.Organization.Queries;
using AiReadinessAssessment.Application.Organization.Queries.Handlers;
using AiReadinessAssessment.Application.Tests.Common;

namespace AiReadinessAssessment.Application.Tests.Organization;

public class ListOrganizationsQueryHandlerTests
{
    private readonly FakeOrganizationRepository _repository = new();
    private readonly ListOrganizationsQueryHandler _handler;

    public ListOrganizationsQueryHandlerTests()
    {
        _handler = new ListOrganizationsQueryHandler(_repository);
    }

    [Fact]
    public async Task Handle_WithNoOrganizations_ReturnsEmptyList()
    {
        var response = await _handler.Handle(new ListOrganizationsQuery());

        response.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_ReturnsMappedOrganizationFields()
    {
        _repository.Seed(Domain.Organization.Organization.Create("Acme Corp"));

        var response = await _handler.Handle(new ListOrganizationsQuery());

        var item = response.Single();
        item.Id.Should().NotBeEmpty();
        item.Name.Should().Be("Acme Corp");
        item.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
    }

    [Fact]
    public async Task Handle_ReturnsOrganizationsOrderedByNameAscending()
    {
        _repository.Seed(Domain.Organization.Organization.Create("Zeta Ltd"));
        _repository.Seed(Domain.Organization.Organization.Create("Alpha Inc"));
        _repository.Seed(Domain.Organization.Organization.Create("Midway Co"));

        var response = await _handler.Handle(new ListOrganizationsQuery());

        response.Select(o => o.Name).Should().BeInAscendingOrder();
    }

    [Fact]
    public async Task Handle_ReturnsAllOrganizations()
    {
        _repository.Seed(Domain.Organization.Organization.Create("Org A"));
        _repository.Seed(Domain.Organization.Organization.Create("Org B"));
        _repository.Seed(Domain.Organization.Organization.Create("Org C"));

        var response = await _handler.Handle(new ListOrganizationsQuery());

        response.Should().HaveCount(3);
    }
}
