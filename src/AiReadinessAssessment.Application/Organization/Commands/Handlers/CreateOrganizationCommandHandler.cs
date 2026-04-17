using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.Organization.Dtos.Responses;
using AiReadinessAssessment.Application.Organization.Services;

namespace AiReadinessAssessment.Application.Organization.Commands.Handlers;

/// <summary>
/// Handler for CreateOrganizationCommand.
/// Creates and persists a new organization.
/// </summary>
public class CreateOrganizationCommandHandler : ICommandHandler<CreateOrganizationCommand, OrganizationResponse>
{
    private readonly IOrganizationRepository _repository;

    /// <summary>
    /// Initializes a new instance of the CreateOrganizationCommandHandler class.
    /// </summary>
    /// <param name="repository">The organization repository.</param>
    public CreateOrganizationCommandHandler(IOrganizationRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Handles the create organization command.
    /// </summary>
    public async Task<OrganizationResponse> Handle(
        CreateOrganizationCommand command,
        CancellationToken cancellationToken = default)
    {
        var organization = Domain.Organization.Organization.Create(command.Name);

        await _repository.AddAsync(organization, cancellationToken);

        return new OrganizationResponse
        {
            Id = organization.Id,
            Name = organization.Name,
            CreatedAt = organization.CreatedAt
        };
    }
}
