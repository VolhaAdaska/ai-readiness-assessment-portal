using FluentValidation;
using AiReadinessAssessment.Application.InitialAssessment.Dtos.Requests;

namespace AiReadinessAssessment.Application.InitialAssessment.Validation;

/// <summary>
/// Validator for CreateInitialAssessmentRequest.
/// </summary>
public class CreateInitialAssessmentValidator : AbstractValidator<CreateInitialAssessmentRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateInitialAssessmentValidator class.
    /// </summary>
    public CreateInitialAssessmentValidator()
    {
           RuleFor(x => x.OrganizationId)
            .NotEmpty()
            .WithMessage("Organization ID cannot be empty.");
    }
}