using System.ComponentModel;

namespace ShipperStation.Domain.Enums;
public enum UserStatus
{
    [Description("Đang hoạt động")]
    Active,

    [Description("Ngưng hoạt động")]
    Inactive,

    [Description("Chưa xác thực")]
    Verifying
}
