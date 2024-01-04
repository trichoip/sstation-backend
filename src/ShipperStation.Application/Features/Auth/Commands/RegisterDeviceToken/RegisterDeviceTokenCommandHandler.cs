using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.Auth.Commands.RegisterDeviceToken;
internal sealed class RegisterDeviceTokenCommandHandler(
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork) : IRequestHandler<RegisterDeviceTokenCommand, MessageResponse>
{
    private readonly IGenericRepository<Token> tokenRepository = unitOfWork.Repository<Token>();

    public async Task<MessageResponse> Handle(RegisterDeviceTokenCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        if (await tokenRepository.ExistsByAsync(_ => _.UserId == userId && _.Value == request.DeviceToken))
        {
            throw new ConflictException(Resource.DeviceTokenAlreadyRegistered);
        }

        var token = new Token
        {
            UserId = userId,
            Type = TokenType.DeviceToken,
            Value = request.DeviceToken
        };

        await tokenRepository.CreateAsync(token);
        await unitOfWork.CommitAsync();

        return new MessageResponse(Resource.DeviceTokenRegisteredSuccess);
    }
}
