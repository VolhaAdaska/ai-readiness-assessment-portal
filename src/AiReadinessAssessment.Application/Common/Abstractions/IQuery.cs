namespace AiReadinessAssessment.Application.Common.Abstractions;

/// <summary>
/// Marker interface for query objects.
/// Queries represent read operations that do not change state.
/// </summary>
/// <typeparam name="TResponse">The response type returned by the query handler.</typeparam>
public interface IQuery<out TResponse>
{
}