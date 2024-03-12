using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Managers.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Managers.Handlers;
internal sealed class DeleteStoreManagerCommandHandle(IUnitOfWork unitOfWork) : IRequestHandler<DeleteStoreManagerCommand, MessageResponse>
{
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();
    public async Task<MessageResponse> Handle(DeleteStoreManagerCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .FindByAsync(x =>
                x.Id == request.Id &&
                x.UserRoles.Any(_ => _.Role.Name == RoleName.StationManager),
            cancellationToken: cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        await _userRepository.DeleteAsync(user);

        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.DeletedSuccess);
    }
}
