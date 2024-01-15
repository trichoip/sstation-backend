using MediatR;
using Microsoft.AspNetCore.Identity;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Users;
using ShipperStation.Application.Features.Wallets.Commands.InitWallet;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Users.Queries.GetProfile;
internal sealed class GetProfileQueryHandler(
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork,
    UserManager<User> userManager,
    IPublisher publisher) : IRequestHandler<GetProfileQuery, UserResponse>
{
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();
    public async Task<UserResponse> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await currentUserService.FindCurrentUserAsync();

        await publisher.Publish(new InitWalletEvent() with { UserId = user.Id }, cancellationToken);

        if (await _userRepository
            .FindByAsync<UserResponse>(_ => _.Id == user.Id, cancellationToken) is not { } userResponse)
        {
            throw new NotFoundException(nameof(User), user);
        }

        userResponse.Roles = (await userManager.GetRolesAsync(user)).ToArray();

        return userResponse;
    }
}
