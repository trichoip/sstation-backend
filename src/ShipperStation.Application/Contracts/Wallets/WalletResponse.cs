namespace ShipperStation.Application.Contracts.Wallets;
public sealed record WalletResponse : BaseAuditableEntityResponse<int>
{
    public decimal Balance { get; set; }
    public DateTimeOffset? LastDepositAt { get; set; }
}
