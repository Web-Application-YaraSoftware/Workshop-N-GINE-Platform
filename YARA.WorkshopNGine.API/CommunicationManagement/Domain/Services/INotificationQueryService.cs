using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Queries;

namespace YARA.WorkshopNGine.API.CommunicationManagement.Domain.Services;

public interface INotificationQueryService
{
    Task<IEnumerable<Notification>> Handle(GetAllNotificationsByUserIdQuery query);
}