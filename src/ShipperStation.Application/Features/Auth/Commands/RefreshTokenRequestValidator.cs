using FluentValidation;
using ShipperStation.Application.Common.Resources;

namespace ShipperStation.Application.Features.Auth.Commands;
public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty().WithMessage(Resource.RefreshTokenRequired);
    }
}
