using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;
using ShipperStation.Infrastructure.Persistence.Data;

namespace ShipperStation.Infrastructure.Persistence.SeedData;

public class ApplicationDbContextInitialiser
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(
       ILogger<ApplicationDbContextInitialiser> logger,
       ApplicationDbContext context,
       UserManager<User> userManager,
       RoleManager<IdentityRole<int>> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task MigrateAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task DeletedDatabaseAsync()
    {
        try
        {
            await _context.Database.EnsureDeletedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        //AccountSeeding.DefaultAccounts

        var adminRole = new IdentityRole<int>(Roles.Admin);
        await _roleManager.CreateAsync(adminRole);
        var admin = new User
        {
            UserName = "admin",
            Email = "00000",
            EmailConfirmed = true,
            Status = UserStatus.Active
        };
        await _userManager.CreateAsync(admin, "admin");
        await _userManager.AddToRolesAsync(admin, new[] { Roles.Admin });

        var userRole = new IdentityRole<int>(Roles.User);
        await _roleManager.CreateAsync(userRole);
        var user = new User
        {
            UserName = "user",
            Email = "00000",
            EmailConfirmed = true,
            Status = UserStatus.Active,
            PhoneNumber = "0123456789"
        };
        await _userManager.CreateAsync(user, "user");
        await _userManager.AddToRolesAsync(user, new[] { Roles.User });

    }
}
