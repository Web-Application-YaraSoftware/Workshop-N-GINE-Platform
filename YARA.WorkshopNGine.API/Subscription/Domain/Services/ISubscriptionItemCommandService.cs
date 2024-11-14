using YARA.WorkshopNGine.API.Subscription.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Commands;

namespace YARA.WorkshopNGine.API.Subscription.Domain.Services;

public interface ISubscriptionItemCommandService
{
    Task<SubscriptionItem?> Handle(CreateSubscriptionItemWithTrialActivateCommand command);
    
    Task<SubscriptionItem?> Handle(CreateSubscriptionItemCommand command);
    
    Task<SubscriptionItem?> Handle(CancelSubscriptionItemCommand command);
}