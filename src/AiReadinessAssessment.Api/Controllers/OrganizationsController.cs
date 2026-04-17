using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.Organization.Commands;
using AiReadinessAssessment.Application.Organization.Dtos.Requests;
using AiReadinessAssessment.Application.Organization.Dtos.Responses;
using AiReadinessAssessment.Application.Organization.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AiReadinessAssessment.Api.Controllers;

/// <summary>
/// API controller for managing organizations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrganizationsController : ControllerBase
{
    private readonly ICommandHandler<CreateOrganizationCommand, OrganizationResponse> _createHandler;
    private readonly IQueryHandler<ListOrganizationsQuery, List<OrganizationResponse>> _listHandler;

    /// <summary>
    /// Initializes a new instance of the OrganizationsController class.
    /// </summary>
    public OrganizationsController(
        ICommandHandler<CreateOrganizationCommand, OrganizationResponse> createHandler,
        IQueryHandler<ListOrganizationsQuery, List<OrganizationResponse>> listHandler)
    {
        _createHandler = createHandler;
        _listHandler = listHandler;
    }

    /// <summary>
    /// Creates a new organization.
    /// </summary>
    /// <param name="request">The create organization request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created organization.</returns>
    /// <response code="201">Organization created successfully.</response>
    /// <response code="400">Invalid request data.</response>
    [HttpPost]
    public async Task<ActionResult<OrganizationResponse>> CreateOrganization(
        [FromBody] CreateOrganizationRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateOrganizationCommand(request.Name);
        var response = await _createHandler.Handle(command, cancellationToken);
        return CreatedAtAction(nameof(ListOrganizations), new { }, response);
    }

    /// <summary>
    /// Retrieves all organizations.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of organizations ordered by name.</returns>
    /// <response code="200">Organizations retrieved successfully.</response>
    [HttpGet]
    public async Task<ActionResult<List<OrganizationResponse>>> ListOrganizations(
        CancellationToken cancellationToken)
    {
        var response = await _listHandler.Handle(new ListOrganizationsQuery(), cancellationToken);
        return Ok(response);
    }
}
