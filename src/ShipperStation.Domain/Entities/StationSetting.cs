using ShipperStation.Domain.Common;
using ShipperStation.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;
public class StationSetting : BaseEntity<int>
{
    public string? CustomValue { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public StationSettingStatus Status { get; set; }

    public int StationId { get; set; }
    public Station Station { get; set; } = default!;

    public int SettingId { get; set; }
    public Setting Setting { get; set; } = default!;
}
