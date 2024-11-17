using YARA.WorkshopNGine.API.Subscription.Domain.Model.ValueObjects;

namespace YARA.WorkshopNGine.API.Subscription.Domain.Model.Commands;

public record CreateSubscriptionItemCommand(WorkshopId WorkshopId, UserId UserId, PlanId PlanId);