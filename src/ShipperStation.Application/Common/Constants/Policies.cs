namespace ShipperStation.Application.Common.Constants;

public abstract class Policies
{
    public const string Admin = nameof(Admin);
    public const string Staff = nameof(Staff);
    public const string StoreManager = nameof(StoreManager);
    public const string User = nameof(User);
    public const string StoreManager_And_Staff = nameof(StoreManager_And_Staff);
    public const string Admin_Or_StationManager = nameof(Admin_Or_StationManager);
}