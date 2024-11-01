using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Entity;
using YARA.WorkshopNGine.API.CommunicationManagement.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.CommunicationManagement.Interfaces.REST.Transform;

public class NotificationStateResourceFromEntityAssembler
{
    public static NotificationStateResource ToResourceFromEntity(NotificationState notificationState)
    {
        return new NotificationStateResource(
            notificationState.Id,
            notificationState.Name);
    }
}