using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ShipperStation.Application.Interfaces.Hubs;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Infrastructure.Hubs;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class TestsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;
    private readonly UserManager<User> _userManager;

    private readonly ICallerSender _smsSender;
    public TestsController(
        IUnitOfWork unitOfWork,
        IJwtService jwtService,
        IHubContext<NotificationHub, INotificationHub> hubContext,
        UserManager<User> userManager,
        ICallerSender smsSender)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _hubContext = hubContext;
        _userManager = userManager;
        _smsSender = smsSender;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _ = _smsSender.SendAsync("0971687136", "000000");
        //var a = await _userManager.CreateAsync(new User
        //{
        //    UserName = "tes2233321222t",
        //    Email = "tes222222t@aad"
        //}, "adad");

        //await _hubContext.Clients.User("08dc05d9-ad55-464f-8615-265d258ecdb7")
        //    .ReceiveNotification(new NotificationResponse { Id = 1 });
        var usr = await _unitOfWork.Repository<User>().FindByAsync(_ => _.UserName == "tes2233321222t");
        return Ok(await _jwtService.GenerateTokenAsync(usr));
    }
}
