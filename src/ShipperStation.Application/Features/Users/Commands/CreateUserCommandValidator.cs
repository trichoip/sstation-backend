using FluentValidation;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Features.Users.Commands;
public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage(Resource.PhoneNumberRequired)
            .Matches(RegexExtensions.PhoneRegex)
            .WithMessage(Resource.PhoneNumberInvalid);
    }
}
