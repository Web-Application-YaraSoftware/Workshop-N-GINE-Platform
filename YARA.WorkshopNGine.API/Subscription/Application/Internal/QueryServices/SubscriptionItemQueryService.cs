using YARA.WorkshopNGine.API.Shared.Domain.Repositories;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Subscription.Domain.Repositories;
using YARA.WorkshopNGine.API.Subscription.Domain.Services;

namespace YARA.WorkshopNGine.API.Subscription.Application.Internal.QueryServices;

public class SubscriptionItemQueryService(ISubscriptionItemRepository subscriptionItemRepository, IUnitOfWork unitOfWork) : ISubscriptionItemQueryService
{
    public async Task<SubscriptionItem?> Handle(GetLatestSubscriptionItemByWorkshopIdQuery query)
    {
        return await subscriptionItemRepository.FindLastByIdAsync(query.WorkshopId);
    }

    public async Task<IEnumerable<SubscriptionItem>> Handle(GetAllSubscriptionItemsByWorkshopIdQuery query)
    {
        return await subscriptionItemRepository.FindAllByWorkshopIdAsync(query.WorkshopId);
    }
}