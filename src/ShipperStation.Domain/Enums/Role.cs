using System.ComponentModel;

namespace ShipperStation.Domain.Enums;

public enum Role
{
    [Description("Quản trị viên")]
    Admin,

    [Description("Nhân viên giặt sấy")]
    LaundryAttendant,

    [Description("Quản lý cửa hàng")]
    Manager,

    [Description("Khách hàng")]
    Customer
}