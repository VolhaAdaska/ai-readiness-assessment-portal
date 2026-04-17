using AiReadinessAssessment.Application.Common.Abstractions;
using AiReadinessAssessment.Application.InitialAssessment.Dtos.Responses;

namespace AiReadinessAssessment.Application.InitialAssessment.Queries;

/// <summary>
/// Query to retrieve a summary list of all assessments for the dashboard.
/// </summary>
public class ListAssessmentsQuery : IQuery<List<AssessmentSummaryResponse>>
{
}
