namespace YARA.WorkshopNGine.API.Subscription.Interfaces.ACL;

public interface ISubscriptionContextFacade
{
    Task<long> CreateSubscriptionWithTrialActive(long workshopId, long userId);
}