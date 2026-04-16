namespace AiReadinessAssessment.Application.Common.Exceptions;

/// <summary>
/// Exception thrown when an entity is not found.
/// </summary>
public class NotFoundException : ApplicationException
{
    /// <summary>
    /// Initializes a new instance of the NotFoundException class.
    /// </summary>
    /// <param name="entityName">The name of the entity type.</param>
    /// <param name="entityId">The ID of the entity that was not found.</param>
    public NotFoundException(string entityName, object entityId)
        : base($"{entityName} with ID {entityId} was not found.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the NotFoundException class with a custom message.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public NotFoundException(string message)
        : base(message)
    {
    }
}