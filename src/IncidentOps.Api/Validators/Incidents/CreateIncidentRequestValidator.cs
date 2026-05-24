using FluentValidation;
using IncidentOps.Api.Contracts.Incidents;

namespace IncidentOps.Api.Validators.Incidents;

public class CreateIncidentRequestValidator : AbstractValidator<CreateIncidentRequest>
{
    public CreateIncidentRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(2000);

        RuleFor(x => x.Severity)
            .NotEmpty()
            .Must(x => x is "Low" or "Medium" or "High" or "Critical")
            .WithMessage("Severity must be Low, Medium, High, or Critical.");
    }
}
