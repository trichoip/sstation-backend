using FluentValidation;

namespace ShipperStation.Application.Features.Auth.Commands.ResetPassword;
public sealed class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.NewPassword)
            .NotEmpty();

        RuleFor(x => x.ResetCode)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(6);
    }
}
