using Microsoft.AspNetCore.Identity;
using ShipperStation.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;
public class UserToken : IdentityUserToken<Guid>
{
    public DateTimeOffset? ExpiredAt { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public TokenType Type { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public TokenStatus Status { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public DeviceType DeviceType { get; set; }
}
