using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Entity;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.ValueoObjects;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.CommunicationManagement.Domain.Repositories;

public interface INotificationStateRepository : IBaseRepository<NotificationState>
{
    bool ExistsByName(ENotificationState name);
}