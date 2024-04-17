using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Staffs.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Staffs.Handlers;
internal sealed class DeleteStaffCommandHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteStaffCommand, MessageResponse>
{
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();
    public async Task<MessageResponse> Handle(DeleteStaffCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .FindByAsync(x =>
                x.UserStations.Any(_ =>
                    _.UserId == request.StaffId &&
                    _.StationId == request.StationId),
             cancellationToken: cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.StaffId);
        }

        await _userRepository.DeleteAsync(user);

        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.DeletedSuccess);
    }
}
