using FluentValidation;

namespace ShipperStation.Application.Contracts.Auth;
public class ResendEmailRequestValidator : AbstractValidator<ResendEmailRequest>
{
    public ResendEmailRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
