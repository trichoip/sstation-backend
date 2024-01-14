using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Domain.Enums;
using ShipperStation.Infrastructure.Persistence.Data;

namespace ShipperStation.Infrastructure.Persistence.SeedData;

public class ApplicationDbContextInitialiser(
   ILogger<ApplicationDbContextInitialiser> logger,
   ApplicationDbContext context,
   UserManager<User> userManager,
   RoleManager<Role> roleManager,
   IUnitOfWork unitOfWork)
{
    public async Task MigrateAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task DeletedDatabaseAsync()
    {
        try
        {
            await context.Database.EnsureDeletedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
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
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {

        if (!await unitOfWork.Repository<Station>().ExistsByAsync())
        {
            await unitOfWork.Repository<Station>().CreateRangeAsync(StationSeed.Default);
            await unitOfWork.CommitAsync();
        }

        if (!await unitOfWork.Repository<Role>().ExistsByAsync())
        {
            foreach (var item in RoleSeed.Default)
            {
                await roleManager.CreateAsync(item);
            }
        }

        if (!await unitOfWork.Repository<User>().ExistsByAsync())
        {
            var user = new User
            {
                UserName = "admin",
                Status = UserStatus.Active
            };
            await userManager.CreateAsync(user, "admin");
            await userManager.AddToRolesAsync(user, new[] { Roles.Admin });

            user = new User
            {
                UserName = "user",
                Status = UserStatus.Active
            };
            await userManager.CreateAsync(user, "user");
            await userManager.AddToRolesAsync(user, new[] { Roles.User });

            user = new User
            {
                UserName = "store",
                Status = UserStatus.Active,
            };
            await userManager.CreateAsync(user, "store");
            await userManager.AddToRolesAsync(user, new[] { Roles.StationManager });
            var station = await unitOfWork.Repository<Station>().FindByAsync(_ => !_.IsDeleted);
            user.UserStations.Add(new UserStation
            {
                UserId = user.Id,
                StationId = station.Id,
            });

            user = new User
            {
                UserName = "staff",
                Status = UserStatus.Active,
            };
            await userManager.CreateAsync(user, "staff");
            await userManager.AddToRolesAsync(user, new[] { Roles.Staff });
            user.UserStations.Add(new UserStation
            {
                UserId = user.Id,
                StationId = station.Id,
            });

            await unitOfWork.CommitAsync();
        }

    }
}
