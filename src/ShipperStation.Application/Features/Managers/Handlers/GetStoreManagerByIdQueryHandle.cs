using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Managers.Queries;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Managers.Handlers;
internal sealed class GetStoreManagerByIdQueryHandle(IUnitOfWork unitOfWork) : IRequestHandler<GetStoreManagerByIdQuery, UserResponse>
{
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();
    public async Task<UserResponse> Handle(GetStoreManagerByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .FindByAsync<UserResponse>(x =>
                x.Id == request.Id &&
                x.UserRoles.Any(_ => _.Role.Name == RoleName.StationManager),
            cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        return user;
    }
}
