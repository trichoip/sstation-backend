using ShipperStation.Domain.Common;
using ShipperStation.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;
public class Setting : BaseEntity<int>
{
    [Column(TypeName = "nvarchar(24)")]
    public SettingKey Key { get; set; } = default!;
    public string Value { get; set; } = default!;

    [Column(TypeName = "nvarchar(24)")]
    public SettingType Type { get; set; } = default!;
    public string? Description { get; set; }

    public virtual ICollection<StationSetting> StationSettings { get; set; } = new HashSet<StationSetting>();
}
