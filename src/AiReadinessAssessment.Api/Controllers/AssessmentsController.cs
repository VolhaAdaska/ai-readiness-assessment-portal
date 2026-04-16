using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.Common.Exceptions;
using AiReadinessAssessment.Application.InitialAssessment.Commands;
using AiReadinessAssessment.Application.InitialAssessment.Dtos.Requests;
using AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;
using AiReadinessAssessment.Application.InitialAssessment.Queries;
using AiReadinessAssessment.Domain.InitialAssessment;
using Microsoft.AspNetCore.Mvc;

namespace AiReadinessAssessment.Api.Controllers;

/// <summary>
/// API controller for managing initial assessments.
/// Provides endpoints for creating, starting, updating, completing, and retrieving assessments.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AssessmentsController : ControllerBase
{
    private readonly ICommandHandler<CreateInitialAssessmentCommand, CreateInitialAssessmentResponse> _createHandler;
    private readonly ICommandHandler<StartInitialAssessmentCommand, StartAssessmentResponse> _startHandler;
    private readonly ICommandHandler<UpdateCategoryAssessmentCommand, UpdateCategoryAssessmentResponse> _updateCategoryHandler;
    private readonly ICommandHandler<CompleteInitialAssessmentCommand, CompleteAssessmentResponse> _completeHandler;
    private readonly IQueryHandler<GetInitialAssessmentQuery, InitialAssessmentResponse> _getAssessmentHandler;
    private readonly IQueryHandler<GetCategoryAssessmentQuery, CategoryAssessmentResponse> _getCategoryHandler;

    /// <summary>
    /// Initializes a new instance of the AssessmentsController class.
    /// </summary>
    public AssessmentsController(
        ICommandHandler<CreateInitialAssessmentCommand, CreateInitialAssessmentResponse> createHandler,
        ICommandHandler<StartInitialAssessmentCommand, StartAssessmentResponse> startHandler,
        ICommandHandler<UpdateCategoryAssessmentCommand, UpdateCategoryAssessmentResponse> updateCategoryHandler,
        ICommandHandler<CompleteInitialAssessmentCommand, CompleteAssessmentResponse> completeHandler,
        IQueryHandler<GetInitialAssessmentQuery, InitialAssessmentResponse> getAssessmentHandler,
        IQueryHandler<GetCategoryAssessmentQuery, CategoryAssessmentResponse> getCategoryHandler)
    {
        _createHandler = createHandler;
        _startHandler = startHandler;
        _updateCategoryHandler = updateCategoryHandler;
        _completeHandler = completeHandler;
        _getAssessmentHandler = getAssessmentHandler;
        _getCategoryHandler = getCategoryHandler;
    }

    /// <summary>
    /// Creates a new initial assessment for an organization.
    /// </summary>
    /// <param name="request">The create assessment request containing the organization ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The newly created assessment details.</returns>
    /// <response code="201">Assessment created successfully.</response>
    /// <response code="400">Invalid request data.</response>
    [HttpPost]
    public async Task<ActionResult<CreateInitialAssessmentResponse>> CreateAssessment(
        [FromBody] CreateInitialAssessmentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateInitialAssessmentCommand(request.OrganizationId);
        var response = await _createHandler.Handle(command, cancellationToken);
        return CreatedAtAction(nameof(GetAssessment), new { id = response.AssessmentId }, response);
    }

    /// <summary>
    /// Starts an initial assessment, transitioning it to InProgress and initializing all categories.
    /// </summary>
    /// <param name="id">The assessment ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated assessment status and initialized categories.</returns>
    /// <response code="200">Assessment started successfully.</response>
    /// <response code="404">Assessment not found.</response>
    [HttpPost("{id}/start")]
    public async Task<ActionResult<StartAssessmentResponse>> StartAssessment(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new StartInitialAssessmentCommand(id);
        var response = await _startHandler.Handle(command, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Updates a category assessment with maturity level and observations.
    /// </summary>
    /// <param name="id">The assessment ID.</param>
    /// <param name="category">The category type to update.</param>
    /// <param name="request">The update request containing maturity level and observations.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated category assessment details.</returns>
    /// <response code="200">Category assessment updated successfully.</response>
    /// <response code="400">Invalid request data.</response>
    /// <response code="404">Assessment or category not found.</response>
    [HttpPut("{id}/categories/{category}")]
    public async Task<ActionResult<UpdateCategoryAssessmentResponse>> UpdateCategoryAssessment(
        Guid id,
        CategoryType category,
        [FromBody] UpdateCategoryAssessmentRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCategoryAssessmentCommand(id, category, request.MaturityLevel, request.Observations);
        var response = await _updateCategoryHandler.Handle(command, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Completes an initial assessment, generating recommendations and calculating final scores.
    /// All categories must be assessed before completion.
    /// </summary>
    /// <param name="id">The assessment ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The completed assessment with generated recommendations and scores.</returns>
    /// <response code="200">Assessment completed successfully.</response>
    /// <response code="404">Assessment not found.</response>
    /// <response code="409">Not all categories are complete.</response>
    [HttpPost("{id}/complete")]
    public async Task<ActionResult<CompleteAssessmentResponse>> CompleteAssessment(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new CompleteInitialAssessmentCommand(id);
        var response = await _completeHandler.Handle(command, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Retrieves a complete initial assessment with all categories and recommendations.
    /// </summary>
    /// <param name="id">The assessment ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The assessment details including all categories and recommendations.</returns>
    /// <response code="200">Assessment retrieved successfully.</response>
    /// <response code="404">Assessment not found.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<InitialAssessmentResponse>> GetAssessment(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetInitialAssessmentQuery(id);
        var response = await _getAssessmentHandler.Handle(query, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Retrieves a specific category assessment from an initial assessment.
    /// </summary>
    /// <param name="id">The assessment ID.</param>
    /// <param name="category">The category type to retrieve.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The category assessment details.</returns>
    /// <response code="200">Category assessment retrieved successfully.</response>
    /// <response code="404">Assessment or category not found.</response>
    [HttpGet("{id}/categories/{category}")]
    public async Task<ActionResult<CategoryAssessmentResponse>> GetCategoryAssessment(
        Guid id,
        CategoryType category,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoryAssessmentQuery(id, category);
        var response = await _getCategoryHandler.Handle(query, cancellationToken);
        return Ok(response);
    }
}
