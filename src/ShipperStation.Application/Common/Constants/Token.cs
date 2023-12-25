namespace ShipperStation.Application.Common.Constants;
public abstract class Token
{
    public const string Bearer = nameof(Bearer);
    public const string ResetPassword = nameof(ResetPassword);
    public const int UpdatePasswordTokenExpireTimeInMinutes = 1;
}
