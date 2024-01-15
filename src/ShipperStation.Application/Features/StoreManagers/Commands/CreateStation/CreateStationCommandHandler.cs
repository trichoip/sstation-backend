using Mapster;
using MediatR;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.StoreManagers.Commands.CreateStation;
internal sealed class CreateStationCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<CreateStationCommand, MessageResponse>
{
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();
    private readonly IGenericRepository<UserStation> _userStationRepository = unitOfWork.Repository<UserStation>();
    private readonly IGenericRepository<StationSetting> _stationSettingRepository = unitOfWork.Repository<StationSetting>();
    private readonly IGenericRepository<Setting> _settingRepository = unitOfWork.Repository<Setting>();

    public async Task<MessageResponse> Handle(CreateStationCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var station = request.Adapt<Station>();

        var userStation = new UserStation
        {
            UserId = userId,
            Station = station
        };

        await _stationRepository.CreateAsync(station, cancellationToken);
        await _userStationRepository.CreateAsync(userStation, cancellationToken);

        var settingDefault = await _settingRepository.FindAsync();
        foreach (var item in settingDefault)
        {
            station.StationSettings.Add(new StationSetting
            {
                SettingId = item.Id,
                Status = StationSettingStatus.Default,
                Value = item.Value
            });
        }

        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.StationCreatedSuccess);
    }
}
