using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.Common.Exceptions;
using AiReadinessAssessment.Application.InitialAssessment.Commands;
using AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;
using AiReadinessAssessment.Application.InitialAssessment.Services;

namespace AiReadinessAssessment.Application.InitialAssessment.Commands.Handlers;

/// <summary>
/// Handler for the UpdateCategoryAssessmentCommand.
/// Updates the maturity level and observations for a specific category within an assessment.
/// </summary>
public class UpdateCategoryAssessmentCommandHandler : ICommandHandler<UpdateCategoryAssessmentCommand, UpdateCategoryAssessmentResponse>
{
    private readonly IInitialAssessmentRepository _repository;

    /// <summary>
    /// Initializes a new instance of the UpdateCategoryAssessmentCommandHandler class.
    /// </summary>
    /// <param name="repository">The assessment repository.</param>
    public UpdateCategoryAssessmentCommandHandler(IInitialAssessmentRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Handles the update category assessment command.
    /// </summary>
    /// <param name="command">The command containing assessment ID, category, maturity level, and observations.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Response containing the updated category assessment details.</returns>
    /// <exception cref="NotFoundException">Thrown if assessment or category not found.</exception>
    public async Task<UpdateCategoryAssessmentResponse> Handle(
        UpdateCategoryAssessmentCommand command,
        CancellationToken cancellationToken = default)
    {
        // Retrieve assessment
        var assessment = await _repository.GetByIdAsync(command.AssessmentId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.InitialAssessment.BaselineAssessment), command.AssessmentId);

        // Find the category assessment
        var categoryAssessment = assessment.CategoryAssessments
            .FirstOrDefault(c => c.Category == command.Category)
            ?? throw new NotFoundException($"Category assessment for {command.Category} not found in assessment {command.AssessmentId}");

        // Update the category (domain validates maturity level range)
        if (command.MaturityLevel.HasValue)
        {
            categoryAssessment.SetMaturityLevel(command.MaturityLevel.Value);
        }
        if (command.Observations != null)
        {
            categoryAssessment.SetObservations(command.Observations);
        }

        // Persist changes
        await _repository.UpdateAsync(assessment, cancellationToken);

        // Map to response DTO
        return new UpdateCategoryAssessmentResponse
        {
            AssessmentId = assessment.Id,
            Category = categoryAssessment.Category,
            MaturityLevel = categoryAssessment.MaturityLevel,
            Observations = categoryAssessment.Observations,
            CategoryScore = categoryAssessment.CalculateCategoryScore(),
            UpdatedAt = DateTime.UtcNow
        };
    }
}
