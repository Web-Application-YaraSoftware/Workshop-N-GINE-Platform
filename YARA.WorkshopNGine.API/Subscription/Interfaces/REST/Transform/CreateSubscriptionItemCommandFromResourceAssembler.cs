using YARA.WorkshopNGine.API.Subscription.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.Subscription.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Subscription.Interfaces.REST.Transform;

public class CreateSubscriptionItemCommandFromResourceAssembler
{
    public static CreateSubscriptionItemCommand ToCommandFromResource(CreateSubscriptionItemResource resource)
    {
        var workshopId = new WorkshopId(resource.WorkshopId);
        var planId = new PlanId(resource.PlanId);
        return new CreateSubscriptionItemCommand(workshopId, planId);
    }
}