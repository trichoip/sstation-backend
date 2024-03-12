using MediatR;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Managers.Queries;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.Managers.Handlers;
internal sealed class GetStoreManagersQueryHandle(IUnitOfWork unitOfWork) : IRequestHandler<GetStoreManagersQuery, PaginatedResponse<UserResponse>>
{
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();
    public async Task<PaginatedResponse<UserResponse>> Handle(GetStoreManagersQuery request, CancellationToken cancellationToken)
    {
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
