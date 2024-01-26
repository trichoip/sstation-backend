using ShipperStation.Domain.Constants;

namespace ShipperStation.Application.Common.Constants;

public abstract class Policies
{
    public const string Admin = $"{Roles.Admin}";
    public const string Staff = $"{Roles.Staff}";
    public const string StationManager = $"{Roles.StationManager}";
    public const string User = $"{Roles.User}";
    public const string StationManager_Or_Staff = $"{Roles.StationManager},{Roles.Staff}";
    public const string Admin_Or_StationManager = nameof(Admin_Or_StationManager);
}