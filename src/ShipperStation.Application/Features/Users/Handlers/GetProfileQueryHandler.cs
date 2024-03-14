using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Application.Features.Users.Queries;
using ShipperStation.Application.Features.Wallets.Events;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Users.Handlers;
internal sealed class GetProfileQueryHandler(
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork,
    IPublisher publisher) : IRequestHandler<GetProfileQuery, UserResponse>
{
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();
    public async Task<UserResponse> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        await publisher.Publish(new InitWalletEvent() with { UserId = userId }, cancellationToken);

        if (await _userRepository
            .FindByAsync<UserResponse>(_ => _.Id == userId, cancellationToken) is not { } userResponse)
        {
            throw new NotFoundException(nameof(User), userId);
        }

        return userResponse;
    }
}
