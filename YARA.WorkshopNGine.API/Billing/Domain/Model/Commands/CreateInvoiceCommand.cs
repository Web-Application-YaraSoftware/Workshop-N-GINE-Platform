namespace YARA.WorkshopNGine.API.Billing.Domain.Model.Commands;

public record CreateInvoiceCommand( long PlanId ,long SubscriptionId, long WorkshopId);