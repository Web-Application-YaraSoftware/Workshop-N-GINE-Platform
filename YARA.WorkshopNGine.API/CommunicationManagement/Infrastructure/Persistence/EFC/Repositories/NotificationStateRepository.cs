using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Entity;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.ValueoObjects;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Repositories;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace YARA.WorkshopNGine.API.CommunicationManagement.Infrastructure.Persistence.EFC.Repositories;

public class NotificationStateRepository (AppDbContext context) : BaseRepository<NotificationState>(context), INotificationStateRepository
{
    public bool ExistsByName(ENotificationState name)
    {
        return Context.Set<NotificationState>().Any(notificationState => notificationState.Name.Equals(name.ToString()));
    }
}