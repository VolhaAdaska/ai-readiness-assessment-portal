namespace AiReadinessAssessment.Domain.InitialAssessment;

/// <summary>
/// Represents a single question-answer pair within a category assessment.
/// Owned by CategoryAssessment; immutable once created.
/// </summary>
public class AssessmentResponse
{
    /// <summary>
    /// Unique identifier for this response.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// External reference to the assessment question (no navigation property).
    /// </summary>
    public Guid QuestionId { get; private set; }

    /// <summary>
    /// The respondent's answer to the question.
    /// </summary>
    public string Answer { get; private set; } = string.Empty;

    /// <summary>
    /// Timestamp when this response was recorded.
    /// </summary>
    public DateTime RecordedAt { get; private set; }

    private AssessmentResponse()
    {
        // EF Core requires parameterless constructor
    }

    /// <summary>
    /// Creates a new assessment response for the specified question.
    /// </summary>
    /// <param name="questionId">External reference to the question.</param>
    /// <param name="answer">The answer text.</param>
    /// <returns>A new AssessmentResponse.</returns>
    /// <exception cref="ArgumentException">Thrown if questionId is empty or answer is empty.</exception>
    public static AssessmentResponse Create(Guid questionId, string answer)
    {
        if (questionId == Guid.Empty)
            throw new ArgumentException("Question ID cannot be empty.", nameof(questionId));

        if (string.IsNullOrWhiteSpace(answer))
            throw new ArgumentException("Answer cannot be empty.", nameof(answer));

        return new AssessmentResponse
        {
            Id = Guid.NewGuid(),
            QuestionId = questionId,
            Answer = answer.Trim(),
            RecordedAt = DateTime.UtcNow
        };
    }
}