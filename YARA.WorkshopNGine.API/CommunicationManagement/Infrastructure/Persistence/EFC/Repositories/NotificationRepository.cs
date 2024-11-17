using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Repositories;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace YARA.WorkshopNGine.API.CommunicationManagement.Infrastructure.Persistence.EFC.Repositories;

public class NotificationRepository(AppDbContext context): BaseRepository<Notification>(context), INotificationRepository
{
    
}