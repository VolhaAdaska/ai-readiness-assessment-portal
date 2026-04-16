using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;
using AiReadinessAssessment.Application.InitialAssessment.Services;

namespace AiReadinessAssessment.Application.InitialAssessment.Commands.Handlers;

/// <summary>
/// Handler for the CreateInitialAssessmentCommand. 
/// Creates a new initial assessment for an organization.
/// </summary>
public class CreateInitialAssessmentCommandHandler : ICommandHandler<CreateInitialAssessmentCommand, CreateInitialAssessmentResponse>
{
    private readonly IInitialAssessmentRepository _repository;

    /// <summary>
    /// Initializes a new instance of the CreateInitialAssessmentCommandHandler class.
    /// </summary>
    /// <param name="repository">The assessment repository.</param>
    public CreateInitialAssessmentCommandHandler(IInitialAssessmentRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Handles the create assessment command.
    /// </summary>
    /// <param name="command">The command containing the organization ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Response containing the newly created assessment ID and status.</returns>
    public async Task<CreateInitialAssessmentResponse> Handle(
        CreateInitialAssessmentCommand command,
        CancellationToken cancellationToken = default)
    {
        // Create new assessment aggregate using factory method
        var assessment = Domain.InitialAssessment.InitialAssessment.Create(command.OrganizationId);

        // Persist to repository
        await _repository.AddAsync(assessment, cancellationToken);

        // Map to response DTO
        return new CreateInitialAssessmentResponse
        {
            AssessmentId = assessment.Id,
            Status = assessment.Status,
            CreatedAt = assessment.CreatedAt
        };
    }
}
