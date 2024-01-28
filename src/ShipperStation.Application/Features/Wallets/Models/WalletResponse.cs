using ShipperStation.Application.Models;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Features.Wallets.Models;
public sealed record WalletResponse : BaseAuditableEntityResponse<int>
{
    public double Balance { get; set; }
    public string FormatBalance => Balance.FormatMoney();
    public DateTimeOffset? LastDepositAt { get; set; }
}
