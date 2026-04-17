using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.Common.Exceptions;
using AiReadinessAssessment.Application.InitialAssessment.Commands;
using AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;
using AiReadinessAssessment.Application.InitialAssessment.Services;

namespace AiReadinessAssessment.Application.InitialAssessment.Commands.Handlers;

/// <summary>
/// Handler for the StartInitialAssessmentCommand.
/// Transitions an assessment from NotStarted to InProgress status.
/// </summary>
public class StartInitialAssessmentCommandHandler : ICommandHandler<StartInitialAssessmentCommand, StartAssessmentResponse>
{
    private readonly IInitialAssessmentRepository _repository;

    /// <summary>
    /// Initializes a new instance of the StartInitialAssessmentCommandHandler class.
    /// </summary>
    /// <param name="repository">The assessment repository.</param>
    public StartInitialAssessmentCommandHandler(IInitialAssessmentRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Handles the start assessment command.
    /// </summary>
    /// <param name="command">The command containing the assessment ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Response containing the updated assessment status and initialized categories.</returns>
    /// <exception cref="NotFoundException">Thrown if assessment not found.</exception>
    public async Task<StartAssessmentResponse> Handle(
        StartInitialAssessmentCommand command,
        CancellationToken cancellationToken = default)
    {
        // Retrieve assessment
        var assessment = await _repository.GetByIdAsync(command.AssessmentId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.InitialAssessment.BaselineAssessment), command.AssessmentId);

        // Start the assessment (domain validates status transition and initializes categories)
        assessment.Start();

        // Persist changes
        await _repository.UpdateAsync(assessment, cancellationToken);

        // Map to response DTO
        return new StartAssessmentResponse
        {
            AssessmentId = assessment.Id,
            Status = assessment.Status,
            StartedAt = assessment.StartedAt ?? DateTime.UtcNow,
            InitializedCategories = assessment.CategoryAssessments
                .Select(c => c.Category)
                .ToList()
        };
    }
}
