using YARA.WorkshopNGine.API.Billing.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Billing.Domain.Model.ValueObjects;

namespace YARA.WorkshopNGine.API.Billing.Domain.Model.Aggregates;

public partial class Invoice
{
    public long Id { get; }
    public int Amount { get; private set; }
    public EInvoiceStatus Status { get; private set; }
    public DateTime IssueDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? PaymentDate { get; private set; }
    public long PlanId { get; private set; }
    public long SubscriptionId { get; private set; }
    public long WorkshopId { get; private set; }
    
    public Invoice() { }
    
    /**
     * 
     * TODO: Create a facade for Amount, SubscriptionId, and PlanId, from the subscription bounded context
     * and WorkshopId from the workshop in the service.
     */
    public Invoice(CreateInvoiceCommand command)
    {
        Amount = 50000;
        Status = EInvoiceStatus.Pending;
        PaymentDate = null;
        IssueDate = DateTime.Now;
        DueDate = IssueDate.AddDays(7);
        PlanId = command.PlanId;
        SubscriptionId = command.SubscriptionId;
        WorkshopId = command.WorkshopId;
    }
    
    
}