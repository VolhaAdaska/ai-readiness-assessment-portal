namespace AiReadinessAssessment.Application.Common.Abstractions;

/// <summary>
/// Marker interface for command objects.
/// Commands represent write operations that change state.
/// </summary>
public interface ICommand
{
}

/// <summary>
/// Marker interface for commands that return a response.
/// </summary>
/// <typeparam name="TResponse">The response type returned by the command handler.</typeparam>
public interface ICommand<out TResponse> : ICommand
{
}