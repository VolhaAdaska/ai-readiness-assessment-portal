using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;

namespace AiReadinessAssessment.Application.InitialAssessment.Queries;

/// <summary>
/// Query to retrieve a complete initial assessment with all related data.
/// </summary>
public class GetInitialAssessmentQuery : IQuery<InitialAssessmentResponse>
{
    /// <summary>
    /// Gets the ID of the assessment to retrieve.
    /// </summary>
    public Guid AssessmentId { get; }

    /// <summary>
    /// Initializes a new instance of the GetInitialAssessmentQuery class.
    /// </summary>
    /// <param name="assessmentId">The assessment ID.</param>
    public GetInitialAssessmentQuery(Guid assessmentId)
    {
        AssessmentId = assessmentId;
    }
}