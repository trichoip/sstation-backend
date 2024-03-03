using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Application.Features.Users.Queries;
using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Users.Handlers;
internal sealed class GetUserByPhoneQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserByPhoneQuery, UserResponse>
{
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();

    public async Task<UserResponse> Handle(GetUserByPhoneQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .FindByAsync<UserResponse>(x =>
                x.PhoneNumber == request.number &&
                x.UserRoles.Any(_ => _.Role.Name == RoleName.User),
            cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.number);
        }

        return user;
    }
}
