using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.CommunicationManagement.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.CommunicationManagement.Interfaces.REST.Transform;

public static class NotificationResourceFromEntityAssembler
{
    public static NotificationResource ToResourceFromEntity(Notification notification)
    {
        return new NotificationResource(
            notification.Id,
            notification.Content,
            notification.Date,
            notification.UserId,
            notification.StateId,
            notification.Endpoints);
    }
    
}