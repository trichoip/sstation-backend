using FluentValidation;

namespace ShipperStation.Application.Features.Users.Commands;
public sealed class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(x => x.NewPassword)
            .NotEmpty();
    }
}
