using YARA.WorkshopNGine.API.Subscription.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Queries;

namespace YARA.WorkshopNGine.API.Subscription.Domain.Services;

public interface ISubscriptionItemQueryService
{
    Task<SubscriptionItem?> Handle(GetSubscriptionItemByWorkshopIdAndStatusIsActiveQuery query);
    
    Task<IEnumerable<SubscriptionItem>> Handle(GetAllSubscriptionItemsByWorkshopIdQuery query);
}