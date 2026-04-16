using AiReadinessAssessment.Domain.Shared;

namespace AiReadinessAssessment.Domain.InitialAssessment;

/// <summary>
/// Aggregate root representing a complete initial AI readiness assessment for an organization.
/// Manages the lifecycle, category assessments, recommendations, and overall scoring.
/// </summary>
public class InitialAssessment
{
    private readonly List<CategoryAssessment> _categoryAssessments = new();
    private readonly List<Recommendation> _recommendations = new();

    /// <summary>
    /// Unique identifier for this assessment.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// The organization being assessed (external reference; no navigation property).
    /// </summary>
    public Guid OrganizationId { get; private set; }

    /// <summary>
    /// Current status of the assessment lifecycle.
    /// </summary>
    public AssessmentStatus Status { get; private set; }

    /// <summary>
    /// Timestamp when the assessment was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Timestamp when the assessment was started; null if not started.
    /// </summary>
    public DateTime? StartedAt { get; private set; }

    /// <summary>
    /// Timestamp when the assessment was completed; null if not completed.
    /// </summary>
    public DateTime? CompletedAt { get; private set; }

    /// <summary>
    /// Read-only collection of category assessments.
    /// </summary>
    public IReadOnlyList<CategoryAssessment> CategoryAssessments => _categoryAssessments.AsReadOnly();

    /// <summary>
    /// Read-only collection of recommendations generated from the assessment.
    /// </summary>
    public IReadOnlyList<Recommendation> Recommendations => _recommendations.AsReadOnly();

    private InitialAssessment()
    {
        // EF Core requires parameterless constructor
    }

    /// <summary>
    /// Creates a new initial assessment for the specified organization.
    /// </summary>
    /// <param name="organizationId">The organization being assessed.</param>
    /// <returns>A new InitialAssessment in NotStarted status.</returns>
    /// <exception cref="ArgumentException">Thrown if organizationId is empty.</exception>
    public static InitialAssessment Create(Guid organizationId)
    {
        if (organizationId == Guid.Empty)
            throw new ArgumentException("Organization ID cannot be empty.", nameof(organizationId));

        return new InitialAssessment
        {
            Id = Guid.NewGuid(),
            OrganizationId = organizationId,
            Status = AssessmentStatus.NotStarted,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Starts the assessment, transitioning it from NotStarted to InProgress.
    /// Initializes all six required category assessments.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if assessment is not in NotStarted status.</exception>
    public void Start()
    {
        if (Status != AssessmentStatus.NotStarted)
            throw new InvalidOperationException(
                $"Assessment can only be started from NotStarted status. Current status: {Status}");

        Status = AssessmentStatus.InProgress;
        StartedAt = DateTime.UtcNow;
        
        // Initialize all 6 required categories for this assessment
        InitializeAllCategories();
    }

    /// <summary>
    /// Completes the assessment, transitioning it from InProgress to Completed.
    /// All six categories must exist and be marked complete.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if assessment is not in InProgress status or validation fails.</exception>
    public void Complete()
    {
        if (Status != AssessmentStatus.InProgress)
            throw new InvalidOperationException(
                $"Assessment can only be completed from InProgress status. Current status: {Status}");

        ValidateAllCategoriesAssessed();

        Status = AssessmentStatus.Completed;
        CompletedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Archives the assessment.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if assessment is not in Completed status.</exception>
    public void Archive()
    {
        if (Status != AssessmentStatus.Completed)
            throw new InvalidOperationException(
                $"Assessment can only be archived from Completed status. Current status: {Status}");

        Status = AssessmentStatus.Archived;
    }

    /// <summary>
    /// Adds a category assessment to this assessment.
    /// </summary>
    /// <param name="categoryAssessment">The category assessment to add.</param>
    /// <exception cref="InvalidOperationException">Thrown if assessment is completed or if category already assessed.</exception>
    public void AddCategoryAssessment(CategoryAssessment categoryAssessment)
    {
        if (categoryAssessment == null)
            throw new ArgumentNullException(nameof(categoryAssessment));

        if (Status == AssessmentStatus.Completed)
            throw new InvalidOperationException("Cannot add category assessments to a completed assessment.");

        if (_categoryAssessments.Any(ca => ca.Category == categoryAssessment.Category))
            throw new InvalidOperationException(
                $"Category {categoryAssessment.Category} has already been assessed.");

        _categoryAssessments.Add(categoryAssessment);
    }

    /// <summary>
    /// Retrieves the category assessment for the specified category type.
    /// </summary>
    /// <param name="category">The category type to retrieve.</param>
    /// <returns>The CategoryAssessment for the specified category, or null if not found.</returns>
    public CategoryAssessment? GetCategoryAssessment(CategoryType category)
    {
        return _categoryAssessments.FirstOrDefault(ca => ca.Category == category);
    }

    /// <summary>
    /// Adds a recommendation to the assessment.
    /// </summary>
    /// <param name="recommendation">The recommendation to add.</param>
    /// <exception cref="InvalidOperationException">Thrown if assessment is not completed.</exception>
    public void AddRecommendation(Recommendation recommendation)
    {
        if (recommendation == null)
            throw new ArgumentNullException(nameof(recommendation));

        if (Status != AssessmentStatus.Completed)
            throw new InvalidOperationException("Recommendations can only be added to completed assessments.");

        _recommendations.Add(recommendation);
    }

    /// <summary>
    /// Calculates the overall readiness score across all category assessments.
    /// </summary>
    /// <returns>Overall readiness score (0-100), or 0 if no categories assessed.</returns>
    public double CalculateOverallReadinessScore()
    {
        if (_categoryAssessments.Count == 0)
            return 0;

        var averageScore = _categoryAssessments.Average(ca => ca.CalculateCategoryScore());
        return Math.Round(averageScore, 2);
    }

    /// <summary>
    /// Determines the overall readiness level based on the calculated score.
    /// </summary>
    /// <returns>The ReadinessLevel classification.</returns>
    public ReadinessLevel DetermineReadinessLevel()
    {
        var score = CalculateOverallReadinessScore();

        return score switch
        {
            < 20 => ReadinessLevel.Critical,
            < 40 => ReadinessLevel.Low,
            < 60 => ReadinessLevel.Moderate,
            < 80 => ReadinessLevel.Good,
            _ => ReadinessLevel.Excellent
        };
    }

    /// <summary>
    /// Gets the number of completed category assessments.
    /// </summary>
    /// <returns>The count of completed categories.</returns>
    public int GetCompletedCategoryCount()
    {
        return _categoryAssessments.Count(ca => ca.IsComplete());
    }

    /// <summary>
    /// Checks if assessment can transition to a specified status.
    /// </summary>
    /// <param name="targetStatus">The status to transition to.</param>
    /// <returns>True if transition is allowed; otherwise false.</returns>
    public bool CanTransitionTo(AssessmentStatus targetStatus)
    {
        return (Status, targetStatus) switch
        {
            (AssessmentStatus.NotStarted, AssessmentStatus.InProgress) => true,
            (AssessmentStatus.InProgress, AssessmentStatus.Completed) => CanComplete(),
            (AssessmentStatus.Completed, AssessmentStatus.Archived) => true,
            _ => false
        };
    }

    /// <summary>
    /// Initializes all six required category assessments for this assessment.
    /// Each organization must assess all categories for AI readiness.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if categories have already been initialized.</exception>
    private void InitializeAllCategories()
    {
        if (_categoryAssessments.Count > 0)
            throw new InvalidOperationException("Categories have already been initialized for this assessment.");

        var allCategories = Enum.GetValues(typeof(CategoryType))
            .Cast<CategoryType>()
            .ToList();

        foreach (var category in allCategories)
        {
            var categoryAssessment = CategoryAssessment.Create(category);
            _categoryAssessments.Add(categoryAssessment);
        }
    }

    /// <summary>
    /// Validates that all six assessment categories exist and are complete.
    /// Called before assessment can be marked as Completed.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if validation fails.</exception>
    private void ValidateAllCategoriesAssessed()
    {
        var allCategories = Enum.GetValues(typeof(CategoryType))
            .Cast<CategoryType>()
            .ToList();

        // Check that all 6 categories are present
        var missingCategories = allCategories
            .Where(c => !_categoryAssessments.Any(ca => ca.Category == c))
            .ToList();

        if (missingCategories.Any())
            throw new InvalidOperationException(
                $"Assessment must include all categories before completion. Missing: {string.Join(", ", missingCategories)}");

        // Check that all categories are marked complete
        var incompleteCategories = _categoryAssessments
            .Where(ca => !ca.IsComplete())
            .Select(ca => ca.Category)
            .ToList();

        if (incompleteCategories.Any())
            throw new InvalidOperationException(
                $"All categories must be completed before assessment completion. Incomplete: {string.Join(", ", incompleteCategories)}");
    }

    /// <summary>
    /// Checks if the assessment can be completed (used by CanTransitionTo for validation).
    /// </summary>
    /// <returns>True if all categories exist and are complete; otherwise false.</returns>
    private bool CanComplete()
    {
        var allCategories = Enum.GetValues(typeof(CategoryType))
            .Cast<CategoryType>()
            .ToList();

        return _categoryAssessments.Count == allCategories.Count &&
               _categoryAssessments.All(ca => ca.IsComplete());
    }
}