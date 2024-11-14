using YARA.WorkshopNGine.API.Subscription.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.Subscription.Domain.Services;

namespace YARA.WorkshopNGine.API.Subscription.Interfaces.ACL.Services;

public class SubscriptionContextFacade(ISubscriptionItemCommandService subscriptionItemCommandService) : ISubscriptionContextFacade
{
    public async Task<long> CreateSubscriptionWithTrialActive(long workshopId, long userId)
    {
        var workshop = new WorkshopId(workshopId);
        var user = new UserId(userId);
        var createSubscriptionWithTrialActive = new CreateSubscriptionItemWithTrialActivateCommand(workshop, user);
        var subscriptionItem = await subscriptionItemCommandService.Handle(createSubscriptionWithTrialActive);
        return subscriptionItem?.Id ?? 0;
    }
}