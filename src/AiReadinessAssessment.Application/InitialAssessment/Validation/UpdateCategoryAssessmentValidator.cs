using FluentValidation;
using AiReadinessAssessment.Application.InitialAssessment.Dtos.Requests;

namespace AiReadinessAssessment.Application.InitialAssessment.Validation;
/// <summary>
/// Validator for UpdateCategoryAssessmentRequest.
/// </summary>
public class UpdateCategoryAssessmentValidator : AbstractValidator<UpdateCategoryAssessmentRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateCategoryAssessmentValidator class.
    /// </summary>
    public UpdateCategoryAssessmentValidator()
    {
        RuleFor(x => x.AssessmentId)
            .NotEmpty()
            .WithMessage("Assessment ID cannot be empty.");

        RuleFor(x => x.MaturityLevel)
            .InclusiveBetween(1, 5)
            .When(x => x.MaturityLevel.HasValue)
            .WithMessage("Maturity level must be between 1 and 5.");

        RuleFor(x => x)
            .Must(x => x.MaturityLevel.HasValue || !string.IsNullOrWhiteSpace(x.Observations))
            .WithMessage("Either maturity level or observations must be provided.");
    }
}