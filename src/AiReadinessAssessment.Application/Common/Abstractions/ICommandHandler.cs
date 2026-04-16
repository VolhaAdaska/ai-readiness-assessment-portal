namespace AiReadinessAssessment.Application.Common.Abstractions;

/// <summary>
/// Handler interface for commands.
/// Implementations handle a specific command and optionally return a response.
/// </summary>
/// <typeparam name="TCommand">The command type to handle.</typeparam>
public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    /// <summary>
    /// Handles the command.
    /// </summary>
    /// <param name="command">The command to handle.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the operation.</returns>
    Task Handle(TCommand command, CancellationToken cancellationToken = default);
}

/// <summary>
/// Handler interface for commands that return a response.
/// </summary>
/// <typeparam name="TCommand">The command type to handle.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    /// <summary>
    /// Handles the command and returns a response.
    /// </summary>
    /// <param name="command">The command to handle.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the operation, with the response result.</returns>
    Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken = default);
}