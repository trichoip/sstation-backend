using MediatR;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Roles.Models;
using ShipperStation.Application.Features.Roles.Queries;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Roles.Handlers;
internal sealed class GetRolesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetRolesQuery, IList<RoleResponse>>
{
    private readonly IGenericRepository<Role> _roleRepository = unitOfWork.Repository<Role>();
    public async Task<IList<RoleResponse>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return await _roleRepository.FindAsync<RoleResponse>(cancellationToken: cancellationToken);
    }
}
