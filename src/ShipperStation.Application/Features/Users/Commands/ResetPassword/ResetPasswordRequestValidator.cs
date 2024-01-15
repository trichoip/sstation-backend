using FluentValidation;

namespace ShipperStation.Application.Features.Auth.Commands.ResetPassword;
public sealed class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(x => x.NewPassword)
            .NotEmpty();
    }
}
