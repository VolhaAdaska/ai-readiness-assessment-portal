using FluentValidation;
using AiReadinessAssessment.Application.InitialAssessment.Dtos.Requests;

namespace AiReadinessAssessment.Application.InitialAssessment.Validation;

/// <summary>
/// Validator for CompleteAssessmentRequest.
/// </summary>
public class CompleteAssessmentValidator : AbstractValidator<CompleteAssessmentRequest>
{
    /// <summary>
    /// Initializes a new instance of the CompleteAssessmentValidator class.
    /// </summary>
    public CompleteAssessmentValidator()
    {
        RuleFor(x => x.AssessmentId)
            .NotEmpty()
            .WithMessage("Assessment ID cannot be empty.");
    }
}