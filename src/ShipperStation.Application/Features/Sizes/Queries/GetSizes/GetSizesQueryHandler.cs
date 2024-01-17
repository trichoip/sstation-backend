using MediatR;
using ShipperStation.Application.Contracts.Sizes;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Sizes.Queries.GetSizes;
internal sealed class GetSizesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetSizesQuery, IList<SizeResponse>>
{
    private readonly IGenericRepository<Size> _sizeRepository = unitOfWork.Repository<Size>();

    public async Task<IList<SizeResponse>> Handle(GetSizesQuery request, CancellationToken cancellationToken)
    {
        return await _sizeRepository.FindAsync<SizeResponse>(cancellationToken: cancellationToken);
    }
}
