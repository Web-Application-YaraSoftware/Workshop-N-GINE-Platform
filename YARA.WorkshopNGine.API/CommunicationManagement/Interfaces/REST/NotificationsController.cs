using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Queries;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Services;
using YARA.WorkshopNGine.API.CommunicationManagement.Interfaces.REST.Transform;

namespace YARA.WorkshopNGine.API.CommunicationManagement.Interfaces.REST;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class NotificationsController(INotificationQueryService notificationQueryService) : ControllerBase
{
    [HttpGet ("{userId}")]
    public async Task<IActionResult> GetNotificationsByUserId(int userId)
    {
        var notifications = await notificationQueryService.Handle(new GetAllNotificationsByUserIdQuery(userId));
        //if (notifications == null) { return NotFound(); }
        var resources = notifications.Select(NotificationResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}