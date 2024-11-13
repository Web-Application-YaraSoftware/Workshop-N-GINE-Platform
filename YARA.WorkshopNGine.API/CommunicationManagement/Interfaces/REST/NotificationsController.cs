using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Queries;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Services;
using YARA.WorkshopNGine.API.CommunicationManagement.Interfaces.REST.Resources;
using YARA.WorkshopNGine.API.CommunicationManagement.Interfaces.REST.Transform;

namespace YARA.WorkshopNGine.API.CommunicationManagement.Interfaces.REST;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class NotificationsController(INotificationQueryService notificationQueryService) : ControllerBase
{
    [HttpGet ]
    [SwaggerResponse( 200, "The Notifications were found", typeof(NotificationResource[]) )]
    public async Task<IActionResult> GetNotificationsByUserId([FromQuery] int userId)
    {
        if (userId == 0) { return BadRequest(); }
        var getAllNotificationsByUserIdQuery = new GetAllNotificationsByUserIdQuery(userId);
        var notifications = await notificationQueryService.Handle(getAllNotificationsByUserIdQuery);
        var resources = notifications.Select(NotificationResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}