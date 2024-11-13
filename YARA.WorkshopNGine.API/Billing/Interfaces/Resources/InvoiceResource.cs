namespace YARA.WorkshopNGine.API.Billing.Interfaces.Resources;

public record InvoiceResource(long Id, int Amount, string Status, DateTime IssueDate, DateTime DueDate, DateTime? PaymentDate, long PlanId, long SubscriptionId, long WorkshopId);