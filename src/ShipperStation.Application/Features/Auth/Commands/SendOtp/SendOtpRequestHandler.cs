using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts;
using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.Auth.Commands.SendOtp;
internal sealed class SendOtpRequestHandler(
    UserManager<User> userManager,
    ILogger<SendOtpRequestHandler> logger,
    IPublisher publisher) : IRequestHandler<SendOtpRequest, MessageResponse>
{
    public async Task<MessageResponse> Handle(SendOtpRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.PhoneNumber);
        if (user is null)
        {
            user = new User
            {
                UserName = request.PhoneNumber,
                PhoneNumber = request.PhoneNumber,
                Status = UserStatus.Active,
                PhoneNumberConfirmed = true
            };

            var result = await userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                throw new ValidationBadRequestException(result.Errors);
            }

            result = await userManager.AddToRoleAsync(user, Roles.User);

            if (!result.Succeeded)
            {
                throw new ValidationBadRequestException(result.Errors);
            }
        }

        if (!await userManager.IsInRoleAsync(user, Roles.User))
        {
            throw new ForbiddenAccessException();
        }

        var code = await userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);

        logger.LogInformation($"OTP: {code} ");

        // send otp to phonenumber in background job
        _ = publisher.Publish(new SendOtpEvent(request.PhoneNumber, code), cancellationToken);

        return new MessageResponse(Resource.OtpSendSuccess);
    }
}
