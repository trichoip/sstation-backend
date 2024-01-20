﻿using System.ComponentModel;

namespace ShipperStation.Domain.Enums;
public enum PackageStatus
{
    [Description("Mới khởi tạo")]
    Initialized,

    [Description("Đang chờ")]
    Waiting,

    [Description("Đã đưa về cửa hàng")]
    Collected,

    [Description("Đang xử lý")]
    Processing,

    [Description("Đã xử lý")]
    Processed,

    [Description("Đã giao")]
    Returned,

    [Description("Đã thanh toán")]
    Completed,

    [Description("Đã hủy")]
    Canceled,

    [Description("Đã đặt chỗ")]
    Reserved,

    [Description("Quá hạn")]
    Overtime
}
