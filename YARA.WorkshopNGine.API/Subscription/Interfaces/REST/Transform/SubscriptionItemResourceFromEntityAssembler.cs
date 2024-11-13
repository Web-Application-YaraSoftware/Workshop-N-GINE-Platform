using YARA.WorkshopNGine.API.Subscription.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Subscription.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Subscription.Interfaces.REST.Transform;

public class SubscriptionItemResourceFromEntityAssembler
{
    public static SubscriptionItemResource ToResourceFromEntity(SubscriptionItem entity)
    {
        return new SubscriptionItemResource(entity.Id, entity.WorkshopId.Value, entity.PlanId.Value, entity.StatusToString(), entity.StartedAt, entity.EndedAt, entity.CancelledAt);
    }
}