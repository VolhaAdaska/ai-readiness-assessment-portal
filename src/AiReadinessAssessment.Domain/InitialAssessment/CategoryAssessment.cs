namespace AiReadinessAssessment.Domain.InitialAssessment;

/// <summary>
/// Represents the assessment data for a single readiness category within an initial assessment.
/// Owned by InitialAssessment; cannot exist independently.
/// </summary>
public class CategoryAssessment
{
    private readonly List<AssessmentResponse> _responses = new();

    /// <summary>
    /// Unique identifier for this category assessment.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// The category being assessed.
    /// </summary>
    public CategoryType Category { get; private set; }

    /// <summary>
    /// Maturity level (1-5) for this category; represents assessed capability level.
    /// Note: Level must be > 0 to consider the category as "assessed and complete".
    /// </summary>
    public int MaturityLevel { get; private set; }

    /// <summary>
    /// Assessor observations and notes for this category.
    /// </summary>
    public string Observations { get; private set; } = string.Empty;

    /// <summary>
    /// Read-only collection of responses for this category.
    /// </summary>
    public IReadOnlyList<AssessmentResponse> Responses => _responses.AsReadOnly();

    private CategoryAssessment()
    {
        // EF Core requires parameterless constructor
    }

    /// <summary>
    /// Creates a new category assessment for the specified category.
    /// </summary>
    /// <param name="category">The category type to assess.</param>
    /// <returns>A new CategoryAssessment with maturity level 0 (not yet assessed).</returns>
    public static CategoryAssessment Create(CategoryType category)
    {
        return new CategoryAssessment
        {
            Id = Guid.NewGuid(),
            Category = category,
            MaturityLevel = 0,
            Observations = string.Empty
        };
    }

    /// <summary>
    /// Sets the maturity level for this category.
    /// </summary>
    /// <param name="level">The maturity level (1-5); represents capability level from basic to mature.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if level is outside 1-5 range for assessed categories.</exception>
    public void SetMaturityLevel(int level)
    {
        if (level < 0 || level > 5)
            throw new ArgumentOutOfRangeException(
                nameof(level), level, "Maturity level must be between 0 and 5.");

        MaturityLevel = level;
    }

    /// <summary>
    /// Sets the assessor observations for this category.
    /// </summary>
    /// <param name="observations">The observations text; can be empty or null.</param>
    public void SetObservations(string? observations)
    {
        Observations = observations ?? string.Empty;
    }

    /// <summary>
    /// Records a response (Q&A) for this category.
    /// </summary>
    /// <param name="questionId">External reference to the question.</param>
    /// <param name="answer">The response answer text.</param>
    /// <exception cref="ArgumentException">Thrown if answer is null or empty, or if question already has a response.</exception>
    public void RecordResponse(Guid questionId, string answer)
    {
        if (questionId == Guid.Empty)
            throw new ArgumentException("Question ID cannot be empty.", nameof(questionId));

        if (string.IsNullOrWhiteSpace(answer))
            throw new ArgumentException("Answer cannot be empty.", nameof(answer));

        if (_responses.Any(r => r.QuestionId == questionId))
            throw new InvalidOperationException(
                $"A response for question {questionId} has already been recorded in this category.");

        var response = AssessmentResponse.Create(questionId, answer);
        _responses.Add(response);
    }

    /// <summary>
    /// Calculates the readiness score for this category (0-100).
    /// Based primarily on maturity level (80%); response count contributes up to 20%.
    /// </summary>
    /// <returns>Score between 0 and 100.</returns>
    public double CalculateCategoryScore()
    {
        // Maturity level contributes 80% of the score
        var maturityScore = (MaturityLevel / 5.0) * 80;

        // Response count contributes up to 20%
        var responseBonus = Math.Min(_responses.Count * 2.5, 20);

        var totalScore = maturityScore + responseBonus;
        return Math.Round(totalScore, 2);
    }

    /// <summary>
    /// Checks if this category assessment is complete.
    /// Complete means: maturity level has been set to 1-5 (indicating assessed capability) and observations recorded.
    /// A category with MaturityLevel = 0 is considered not yet assessed.
    /// </summary>
    /// <returns>True if category is complete and assessed; otherwise false.</returns>
    public bool IsComplete()
    {
        return MaturityLevel > 0 && !string.IsNullOrWhiteSpace(Observations);
    }

    /// <summary>
    /// Gets the total number of responses recorded for this category.
    /// </summary>
    /// <returns>The count of question-answer responses.</returns>
    public int GetResponseCount()
    {
        return _responses.Count;
    }
}