namespace ShipperStation.Application.Features.PackageFeature.Models;
public sealed record InfoStaffGennerateQrPaymentModel
{
    public Guid PackageId { get; init; }
    public Guid StaffId { get; init; }
}
