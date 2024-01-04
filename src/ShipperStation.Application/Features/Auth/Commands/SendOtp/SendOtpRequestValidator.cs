using FluentValidation;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Features.Auth.Commands.SendOtp;
public sealed class SendOtpRequestValidator : AbstractValidator<SendOtpRequest>
{
    public SendOtpRequestValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage(Resource.PhoneNumberRequired)
            .Matches(RegexExtensions.PhoneRegex)
            .WithMessage(Resource.PhoneNumberInvalid);
    }
}
