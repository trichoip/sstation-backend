using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Sizes.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Sizes.Handlers;
internal sealed class UpdateSizeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateSizeCommand, MessageResponse>
{
    private readonly IGenericRepository<Size> _sizeRepository = unitOfWork.Repository<Size>();
    public async Task<MessageResponse> Handle(UpdateSizeCommand request, CancellationToken cancellationToken)
    {
        var size = await _sizeRepository
            .FindByAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (size == null)
        {
            throw new NotFoundException(nameof(Size), request.Id);
        }

        request.Adapt(size);

        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.SizeUpdatedSuccess);
    }
}
