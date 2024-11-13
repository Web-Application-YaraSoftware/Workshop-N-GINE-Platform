using YARA.WorkshopNGine.API.Billing.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Billing.Interfaces.Resources;

namespace YARA.WorkshopNGine.API.Billing.Interfaces.Transform;

public class CreateInvoiceCommandFromResourceAssembler
{
    public static CreateInvoiceCommand ToCommandFromResource(CreateInvoiceResource resource)
    {
        return new CreateInvoiceCommand(
            resource.PlanId,
            resource.SubscriptionId, 
            resource.WorkshopId
        );
    }
}