using YARA.WorkshopNGine.API.Shared.Domain.Repositories;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Aggregates;

namespace YARA.WorkshopNGine.API.Subscription.Domain.Repositories;

public interface ISubscriptionItemRepository : IBaseRepository<SubscriptionItem>
{
    Task<SubscriptionItem?> FindLastByWorkshopIdAndUserIdAsync(long workshopId, long userId);
    
    Task<SubscriptionItem?> FindLastByIdAsync(long id);
    
    bool ExitsByWorkshopIdAndUserIdAndIsTrialAsync(long workshopId, long userId);
    
    Task<IEnumerable<SubscriptionItem>> FindAllByWorkshopIdAsync(long workshopId);
}