using ShipperStation.Domain.Constants;

namespace ShipperStation.Application.Common.Constants;

public abstract class Policies
{
    public const string Admin = $"{RoleName.Admin}";
    public const string Staff = $"{RoleName.Staff}";
    public const string StationManager = $"{RoleName.StationManager}";
    public const string User = $"{RoleName.User}";
    public const string StationManager_Or_Staff = $"{RoleName.StationManager},{RoleName.Staff}";
    public const string Admin_Or_StationManager = nameof(Admin_Or_StationManager);
}