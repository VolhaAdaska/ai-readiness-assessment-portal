using AiReadinessAssessment.Application.InitialAssessment.Queries;
using AiReadinessAssessment.Application.InitialAssessment.Queries.Handlers;
using AiReadinessAssessment.Application.Tests.Common;
using AiReadinessAssessment.Domain.InitialAssessment;
using AiReadinessAssessment.Domain.Shared;

namespace AiReadinessAssessment.Application.Tests.InitialAssessment;

public class ListAssessmentsQueryHandlerTests
{
    private readonly FakeAssessmentRepository _assessmentRepository = new();
    private readonly FakeOrganizationRepository _organizationRepository = new();
    private readonly ListAssessmentsQueryHandler _handler;

    public ListAssessmentsQueryHandlerTests()
    {
        _handler = new ListAssessmentsQueryHandler(_assessmentRepository, _organizationRepository);
    }

    private Domain.Organization.Organization SeedOrganization(string name)
    {
        var org = Domain.Organization.Organization.Create(name);
        _organizationRepository.Seed(org);
        return org;
    }

    private BaselineAssessment SeedAssessment(Guid organizationId)
    {
        var assessment = BaselineAssessment.Create(organizationId);
        _assessmentRepository.Seed(assessment);
        return assessment;
    }

    [Fact]
    public async Task Handle_WithNoAssessments_ReturnsEmptyList()
    {
        var response = await _handler.Handle(new ListAssessmentsQuery());

        response.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_ResolvesOrganizationNameFromOrganizationId()
    {
        var org = SeedOrganization("Acme Corp");
        SeedAssessment(org.Id);

        var response = await _handler.Handle(new ListAssessmentsQuery());

        response.Single().OrganizationName.Should().Be("Acme Corp");
        response.Single().OrganizationId.Should().Be(org.Id);
    }

    [Fact]
    public async Task Handle_WhenOrganizationNotFound_ReturnsEmptyOrganizationName()
    {
        SeedAssessment(Guid.NewGuid()); // no matching org in store

        var response = await _handler.Handle(new ListAssessmentsQuery());

        response.Single().OrganizationName.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_ReturnsAssessmentsOrderedByCreatedAtDescending()
    {
        var org = SeedOrganization("Org");

        // Seed in ascending order; result should be reversed
        var first = BaselineAssessment.Create(org.Id);
        var second = BaselineAssessment.Create(org.Id);
        _assessmentRepository.Seed(first);
        _assessmentRepository.Seed(second);

        var response = await _handler.Handle(new ListAssessmentsQuery());

        // second was created after first — should appear first
        response.First().AssessmentId.Should().Be(second.Id);
        response.Last().AssessmentId.Should().Be(first.Id);
    }

    [Fact]
    public async Task Handle_ForNotStartedAssessment_ReturnsZeroProgressFields()
    {
        var org = SeedOrganization("Org");
        SeedAssessment(org.Id);

        var response = await _handler.Handle(new ListAssessmentsQuery());

        var item = response.Single();
        item.TotalCategories.Should().Be(0);
        item.CompletedCategories.Should().Be(0);
        item.CompletionPercentage.Should().Be(0);
        item.ReadinessLevel.Should().BeNull();
        item.OverallScore.Should().BeNull();
    }

    [Fact]
    public async Task Handle_ForStartedAssessment_ReturnsSixTotalCategories()
    {
        var org = SeedOrganization("Org");
        var assessment = BaselineAssessment.Create(org.Id);
        assessment.Start();
        _assessmentRepository.Seed(assessment);

        var response = await _handler.Handle(new ListAssessmentsQuery());

        response.Single().TotalCategories.Should().Be(6);
        response.Single().CompletedCategories.Should().Be(0);
        response.Single().CompletionPercentage.Should().Be(0);
    }

    [Fact]
    public async Task Handle_ForPartiallyAssessedAssessment_ReturnsCorrectCompletionPercentage()
    {
        var org = SeedOrganization("Org");
        var assessment = BaselineAssessment.Create(org.Id);
        assessment.Start();

        // Complete 3 of 6 categories
        foreach (var category in assessment.CategoryAssessments.Take(3))
        {
            category.SetMaturityLevel(3);
            category.SetObservations("Assessed");
        }

        _assessmentRepository.Seed(assessment);

        var response = await _handler.Handle(new ListAssessmentsQuery());

        var item = response.Single();
        item.CompletedCategories.Should().Be(3);
        item.TotalCategories.Should().Be(6);
        item.CompletionPercentage.Should().Be(50);
    }

    [Fact]
    public async Task Handle_ForCompletedAssessment_ReturnsReadinessLevelAndScore()
    {
        var org = SeedOrganization("Org");
        var assessment = BaselineAssessment.Create(org.Id);
        assessment.Start();

        foreach (var category in assessment.CategoryAssessments)
        {
            category.SetMaturityLevel(5);
            category.SetObservations("Excellent");
        }

        assessment.Complete();
        _assessmentRepository.Seed(assessment);

        var response = await _handler.Handle(new ListAssessmentsQuery());

        var item = response.Single();
        item.ReadinessLevel.Should().NotBeNull();
        item.OverallScore.Should().BeGreaterThan(0);
        item.CompletionPercentage.Should().Be(100);
    }
}
