using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.PackageStatusHistories.Models;
using ShipperStation.Application.Features.PackageStatusHistories.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.PackageStatusHistories.Handlers;
internal sealed class GetPackageStatusHistoryByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPackageStatusHistoryByIdQuery, PackageStatusHistoryResponse>
{
    private readonly IGenericRepository<PackageStatusHistory> _packageStatusHistoryRepository = unitOfWork.Repository<PackageStatusHistory>();
    public async Task<PackageStatusHistoryResponse> Handle(GetPackageStatusHistoryByIdQuery request, CancellationToken cancellationToken)
    {

        var packageStatusHistory = await _packageStatusHistoryRepository
            .FindByAsync<PackageStatusHistoryResponse>(_ => _.Id == request.Id, cancellationToken);

        if (packageStatusHistory is null)
        {
            throw new NotFoundException(nameof(PackageStatusHistory), request.Id);
        }

        return packageStatusHistory;
    }
}
