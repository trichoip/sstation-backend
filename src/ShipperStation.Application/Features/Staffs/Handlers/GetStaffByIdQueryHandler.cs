using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Staffs.Queries;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Staffs.Handlers;
internal sealed class GetStaffByIdQueryHandler(
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork) : IRequestHandler<GetStaffByIdQuery, UserResponse>
{
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();
    public async Task<UserResponse> Handle(GetStaffByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var user = await _userRepository
            .FindByAsync<UserResponse>(x =>
                x.UserStations.Any(_ =>
                    _.UserId == request.StaffId &&
                    _.StationId == request.StationId &&
                    _.Station.UserStations.Any(_ => _.UserId == userId)),
            cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.StaffId);
        }

        return user;
    }
}
