using YARA.WorkshopNGine.API.Subscription.Domain.Model.ValueObjects;

namespace YARA.WorkshopNGine.API.Subscription.Domain.Model.Commands;

public record CreateSubscriptionItemWithTrialActivateCommand(WorkshopId WorkshopId, UserId UserId);