using System.ComponentModel;

namespace ShipperStation.Domain.Enums;

public enum OrderType
{
    [Description("Giặt sấy")]
    Laundry,

    [Description("Giữ đồ")]
    Storage
}