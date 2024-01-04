using FluentValidation;

namespace ShipperStation.Application.Features.Auth.Commands.ResendEmail;
public class ResendEmailRequestValidator : AbstractValidator<ResendEmailRequest>
{
    public ResendEmailRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
