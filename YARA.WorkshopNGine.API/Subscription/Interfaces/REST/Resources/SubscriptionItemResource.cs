namespace YARA.WorkshopNGine.API.Subscription.Interfaces.REST.Resources;

public record SubscriptionItemResource(long Id, long WorkshopId, long UserId, long PlanId, string Status, DateTime? StartedAt, DateTime? EndedAt, DateTime? CancelledAt, bool IsTrial, DateTime? TrialEndsAt);