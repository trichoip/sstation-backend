using System.ComponentModel;

namespace ShipperStation.Domain.Enums;
public enum PackageStatus
{
    [Description("Mới khởi tạo")]
    Initialized,

    [Description("Đã trả hàng")]
    Returned,

    [Description("Đã thanh toán")]
    Paid,

    [Description("Đã nhận")]
    Completed,

    [Description("Đã hủy")]
    Canceled,

    [Description("Đã hết hạn")]
    Expired
}
