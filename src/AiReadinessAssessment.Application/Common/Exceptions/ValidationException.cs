namespace AiReadinessAssessment.Application.Common.Exceptions;

/// <summary>
/// Exception thrown when request validation fails.
/// </summary>
public class ValidationException : ApplicationException
{
    /// <summary>
    /// Gets the validation errors keyed by property name.
    /// </summary>
    public IReadOnlyDictionary<string, string[]> Errors { get; }

    /// <summary>
    /// Initializes a new instance of the ValidationException class.
    /// </summary>
    /// <param name="errors">Dictionary of validation errors by property name.</param>
    public ValidationException(IReadOnlyDictionary<string, string[]> errors)
        : base("One or more validation failures have occurred.")
    {
        Errors = errors;
    }

    /// <summary>
    /// Initializes a new instance of the ValidationException class with a simple error.
    /// </summary>
    /// <param name="propertyName">The property name that failed validation.</param>
    /// <param name="errorMessage">The validation error message.</param>
    public ValidationException(string propertyName, string errorMessage)
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>
        {
            { propertyName, new[] { errorMessage } }
        };
    }
}