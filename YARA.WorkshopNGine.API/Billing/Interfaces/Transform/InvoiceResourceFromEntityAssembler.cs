using YARA.WorkshopNGine.API.Billing.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Billing.Interfaces.Resources;

namespace YARA.WorkshopNGine.API.Billing.Interfaces.Transform;

public class InvoiceResourceFromEntityAssembler
{
    public static InvoiceResource ToResourceFromEntity(Invoice invoice)
    {
        return new InvoiceResource(invoice.Id, invoice.Amount, invoice.Status.ToString(), invoice.IssueDate, invoice.DueDate, invoice.PaymentDate, invoice.PlanId, invoice.SubscriptionId, invoice.WorkshopId);
    }
}