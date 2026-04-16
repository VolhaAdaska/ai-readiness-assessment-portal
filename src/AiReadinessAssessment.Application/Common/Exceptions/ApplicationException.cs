namespace AiReadinessAssessment.Application.Common.Exceptions;

/// <summary>
/// Base exception for application layer errors.
/// </summary>
public class ApplicationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the ApplicationException class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public ApplicationException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the ApplicationException class with an inner exception.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception.</param>
    public ApplicationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}