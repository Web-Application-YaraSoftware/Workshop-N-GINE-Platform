namespace YARA.WorkshopNGine.API.Subscription.Interfaces.REST.Resources;

public record SubscriptionItemResource(long Id, long WorkshopId, long PlanId, string Status, DateTime StartedAt, DateTime? EndedAt, DateTime? CancelledAt);