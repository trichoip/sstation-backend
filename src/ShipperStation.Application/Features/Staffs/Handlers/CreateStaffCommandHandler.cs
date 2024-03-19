using MediatR;
using Microsoft.AspNetCore.Identity;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Staffs.Commands;
using ShipperStation.Application.Features.Wallets.Events;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Staffs.Handlers;
internal sealed class CreateStaffCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService,
    UserManager<User> userManager,
    IPublisher publisher) : IRequestHandler<CreateStaffCommand, MessageResponse>
{
    private readonly IGenericRepository<UserStation> _userStationRepository = unitOfWork.Repository<UserStation>();
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();

    public async Task<MessageResponse> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
    {
        var storeManagerId = await currentUserService.FindCurrentUserIdAsync();

        if (!await _stationRepository.ExistsByAsync(
            _ => _.Id == request.StationId &&
                 _.UserStations.Any(_ => _.UserId == storeManagerId),
            cancellationToken))
        {
            throw new NotFoundException(nameof(Station), request.StationId);
        }

        var staff = new User
        {
            UserName = request.UserName,
            FullName = request.FullName,
            IsActive = true,
        };

        var result = await userManager.CreateAsync(staff, request.Password);

        if (!result.Succeeded)
        {
            throw new ValidationBadRequestException(result.Errors);
        }

        result = await userManager.AddToRolesAsync(staff, new[] { RoleName.Staff });

        if (!result.Succeeded)
        {
            throw new ValidationBadRequestException(result.Errors);
        }

        var stationUser = new UserStation
        {
            StationId = request.StationId,
            UserId = staff.Id
        };

        await _userStationRepository.CreateAsync(stationUser, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        await publisher.Publish(new InitWalletEvent() with { UserId = staff.Id }, cancellationToken);

        return new MessageResponse(Resource.StaffCreatedSuccess);
    }
}
