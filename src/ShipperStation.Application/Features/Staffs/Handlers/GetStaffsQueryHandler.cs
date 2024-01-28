using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Staffs.Queries;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.Staffs.Handlers;
internal sealed class GetStaffsQueryHandler(
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork) : IRequestHandler<GetStaffsQuery, PaginatedResponse<UserResponse>>
{
    private readonly IGenericRepository<UserStation> _userStationRepository = unitOfWork.Repository<UserStation>();
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();

    public async Task<PaginatedResponse<UserResponse>> Handle(GetStaffsQuery request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        if (!await _stationRepository.ExistsByAsync(_ => _.Id == request.StationId))
        {
            throw new NotFoundException(nameof(Station), request.StationId);
        }

        if (!await _userStationRepository
            .ExistsByAsync(_ =>
            _.UserId == userId &&
            _.StationId == request.StationId))
        {
            throw new BadRequestException(Resource.UserNotInStation);
        }

        var users = await _userRepository
            .FindAsync<UserResponse>(
                request.PageIndex,
                request.PageSize,
                request.GetExpressions(),
                request.GetOrder(),
                cancellationToken);

        return await users.ToPaginatedResponseAsync();
    }
}
