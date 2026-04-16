namespace AiReadinessAssessment.Application.Common.Abstractions;

/// <summary>
/// Handler interface for queries.
/// </summary>
/// <typeparam name="TQuery">The query type to handle.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    /// <summary>
    /// Handles the query and returns a response.
    /// </summary>
    /// <param name="query">The query to handle.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the operation, with the response result.</returns>
    Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken = default);
}