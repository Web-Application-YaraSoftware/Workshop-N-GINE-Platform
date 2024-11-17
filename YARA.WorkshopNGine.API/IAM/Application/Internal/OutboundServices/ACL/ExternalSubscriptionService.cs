using YARA.WorkshopNGine.API.IAM.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.Subscription.Interfaces.ACL;

namespace YARA.WorkshopNGine.API.IAM.Application.Internal.OutboundServices.ACL;

public class ExternalSubscriptionService(ISubscriptionContextFacade subscriptionContextFacade)
{
    public async Task<SubscriptionId?> CreateSubscriptionWithTrialActive(long workshopId, long userId)
    {
        var subscriptionId = await subscriptionContextFacade.CreateSubscriptionWithTrialActive(workshopId, userId);
        if (subscriptionId == 0) return await Task.FromResult<SubscriptionId?>(null);
        return new SubscriptionId(subscriptionId);
    }
}