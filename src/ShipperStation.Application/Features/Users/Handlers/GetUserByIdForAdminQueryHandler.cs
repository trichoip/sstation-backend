using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Application.Features.Users.Queries;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Users.Handlers;
internal sealed class GetUserByIdForAdminQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserByIdForAdminQuery, UserResponse>
{
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();
    public async Task<UserResponse> Handle(GetUserByIdForAdminQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .FindByAsync<UserResponse>(x => x.Id == request.Id, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        return user;
    }
}
