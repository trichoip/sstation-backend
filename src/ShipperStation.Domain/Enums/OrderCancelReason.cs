using System.ComponentModel;

namespace ShipperStation.Domain.Enums;

public enum OrderCancelReason
{
    [Description("Hết thời gian chờ")]
    Timeout,

    [Description("Khách hàng hủy")]
    CustomerCancel,

    [Description("Nhân viên hủy")]
    StaffCancel,

    [Description("Hủy cọc")]
    ReservationCancel
}