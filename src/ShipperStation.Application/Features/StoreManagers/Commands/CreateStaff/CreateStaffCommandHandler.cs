using MediatR;
using Microsoft.AspNetCore.Identity;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.StoreManagers.Commands.CreateStaff;
internal sealed class CreateStaffCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    UserManager<User> userManager) : IRequestHandler<CreateStaffCommand, MessageResponse>
{
    private readonly IGenericRepository<UserStation> _userStationRepository = unitOfWork.Repository<UserStation>();
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();

    public async Task<MessageResponse> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
    {
        var storeManagerId = await currentUserService.FindCurrentUserIdAsync();

        if (!await _stationRepository.ExistsByAsync(_ => _.Id == request.StationId))
        {
            throw new NotFoundException(nameof(Station), request.StationId);
        }

        if (!await _userStationRepository
            .ExistsByAsync(_ =>
            _.UserId == storeManagerId &&
            _.StationId == request.StationId))
        {
            throw new BadRequestException(Resource.UserNotInStation);
        }

        var staff = new User
        {
            UserName = request.UserName,
            FullName = request.FullName,
            Status = UserStatus.Active
        };

        var result = await userManager.CreateAsync(staff, request.Password);

        if (!result.Succeeded)
        {
            throw new ValidationBadRequestException(result.Errors);
        }

        result = await userManager.AddToRolesAsync(staff, new[] { Roles.Staff });

        if (!result.Succeeded)
        {
            throw new ValidationBadRequestException(result.Errors);
        }

        var stationUser = new UserStation
        {
            StationId = request.StationId,
            UserId = staff.Id
        };

        await _userStationRepository.CreateAsync(stationUser);
        await unitOfWork.CommitAsync();

        return new MessageResponse(Resource.StaffCreatedSuccess);
    }
}
