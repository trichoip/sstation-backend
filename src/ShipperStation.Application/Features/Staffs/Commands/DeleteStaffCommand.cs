using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Staffs.Commands;
public sealed record DeleteStaffCommand(Guid StaffId) : IRequest<MessageResponse>
{
    public int StationId { get; set; }
}

