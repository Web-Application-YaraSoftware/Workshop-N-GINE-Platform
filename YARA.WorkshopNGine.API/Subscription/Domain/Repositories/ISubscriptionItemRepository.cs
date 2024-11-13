using YARA.WorkshopNGine.API.Shared.Domain.Repositories;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Aggregates;

namespace YARA.WorkshopNGine.API.Subscription.Domain.Repositories;

public interface ISubscriptionItemRepository : IBaseRepository<SubscriptionItem>
{
    Task<SubscriptionItem?> FindByWorkshopIdAndStatusIsActiveAsync(long workshopId);
    
    Task<IEnumerable<SubscriptionItem>> FindAllByWorkshopIdAsync(long workshopId);
    
    bool ExistsByWorkshopIdAndStatusIsActive(long workshopId);
}