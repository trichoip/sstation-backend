using FluentValidation;
using ShipperStation.Application.Common.Resources;

namespace ShipperStation.Application.Features.Auth.Commands.Login;
public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage(Resource.UsernameRequired);
        RuleFor(x => x.Password).NotEmpty().WithMessage(Resource.PasswordRequired);
    }
}
