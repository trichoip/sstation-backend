using MediatR;
using ShipperStation.Application.Features.Users.Models;

namespace ShipperStation.Application.Features.Staffs.Queries;
public sealed record GetStaffByIdQuery(Guid StaffId) : IRequest<UserResponse>
{
    public int StationId { get; set; }
}

