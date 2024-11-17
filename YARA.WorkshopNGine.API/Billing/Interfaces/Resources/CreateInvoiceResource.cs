namespace YARA.WorkshopNGine.API.Billing.Interfaces.Resources;

public record CreateInvoiceResource( long PlanId, long SubscriptionId, long WorkshopId);