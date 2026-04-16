using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.Common.Exceptions;
using AiReadinessAssessment.Application.InitialAssessment.Commands;
using AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;
using AiReadinessAssessment.Application.InitialAssessment.Services;

namespace AiReadinessAssessment.Application.InitialAssessment.Commands.Handlers;

/// <summary>
/// Handler for the CompleteInitialAssessmentCommand.
/// Completes an assessment, generates recommendations, and transitions status to Completed.
/// </summary>
public class CompleteInitialAssessmentCommandHandler : ICommandHandler<CompleteInitialAssessmentCommand, CompleteAssessmentResponse>
{
    private readonly IInitialAssessmentRepository _repository;
    private readonly IRecommendationGenerator _recommendationGenerator;

    /// <summary>
    /// Initializes a new instance of the CompleteInitialAssessmentCommandHandler class.
    /// </summary>
    /// <param name="repository">The assessment repository.</param>
    /// <param name="recommendationGenerator">The recommendation generation service.</param>
    public CompleteInitialAssessmentCommandHandler(
        IInitialAssessmentRepository repository,
        IRecommendationGenerator recommendationGenerator)
    {
        _repository = repository;
        _recommendationGenerator = recommendationGenerator;
    }

    /// <summary>
    /// Handles the complete assessment command.
    /// </summary>
    /// <param name="command">The command containing the assessment ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Response containing the completed assessment, overall score, and generated recommendations.</returns>
    /// <exception cref="NotFoundException">Thrown if assessment not found.</exception>
    /// <exception cref="InvalidOperationException">Thrown if not all categories are complete.</exception>
    public async Task<CompleteAssessmentResponse> Handle(
        CompleteInitialAssessmentCommand command,
        CancellationToken cancellationToken = default)
    {
        // Retrieve assessment
        var assessment = await _repository.GetByIdAsync(command.AssessmentId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.InitialAssessment.InitialAssessment), command.AssessmentId);

        // Generate recommendations based on current assessment state
        var recommendations = await _recommendationGenerator.GenerateRecommendationsAsync(assessment, cancellationToken);

        // Add recommendations to assessment
        foreach (var recommendation in recommendations)
        {
            assessment.AddRecommendation(recommendation);
        }

        // Complete the assessment (domain validates all categories are assessed)
        assessment.Complete();

        // Persist changes
        await _repository.UpdateAsync(assessment, cancellationToken);

        // Build category scores dictionary
        var categoryScores = assessment.CategoryAssessments
            .ToDictionary(c => c.Category, c => c.CalculateCategoryScore());

        // Map recommendations to response DTOs
        var recommendationResponses = recommendations
            .Select(r => new RecommendationResponse
            {
                Id = r.Id,
                Category = r.Category,
                Priority = r.Priority,
                Title = r.Title,
                Description = r.Description,
                CreatedAt = r.CreatedAt
            })
            .ToList();

        // Map to response DTO
        return new CompleteAssessmentResponse
        {
            AssessmentId = assessment.Id,
            Status = assessment.Status,
            CompletedAt = assessment.CompletedAt ?? DateTime.UtcNow,
            OverallScore = assessment.CalculateOverallReadinessScore(),
            ReadinessLevel = assessment.DetermineReadinessLevel(),
            CategoryScores = categoryScores,
            RecommendationCount = recommendations.Count
        };
    }
}
