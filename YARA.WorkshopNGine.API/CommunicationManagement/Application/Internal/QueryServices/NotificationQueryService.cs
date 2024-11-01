using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Queries;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Repositories;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Services;

namespace YARA.WorkshopNGine.API.CommunicationManagement.Application.Internal.QueryServices;

public class NotificationQueryService(INotificationRepository notificationRepository) : INotificationQueryService
{
    public async Task<IEnumerable<Notification>> Handle(GetAllNotificationsByUserIdQuery query)
    {
        var notifications = await notificationRepository.ListAsync();
        return notifications.Where(n => n.UserId == query.userId).ToList();
    }
}