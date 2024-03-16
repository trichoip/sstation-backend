using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Application.Features.UserStations.Queries;
using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.UserStations.Handlers;
internal sealed class GetManagerInStationQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetManagerInStationQuery, UserResponse>
{
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();
    public async Task<UserResponse> Handle(GetManagerInStationQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .FindByAsync<UserResponse>(x =>
                x.Id == request.Id &&
                x.UserStations.Any(_ => _.StationId == request.StationId) &&
                x.UserRoles.Any(_ => _.Role.Name == RoleName.StationManager),
            cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        return user;
    }
}
