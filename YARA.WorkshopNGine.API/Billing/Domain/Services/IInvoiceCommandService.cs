using YARA.WorkshopNGine.API.Billing.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Billing.Domain.Model.Commands;

namespace YARA.WorkshopNGine.API.Billing.Domain.Services;

public interface IInvoiceCommandService
{
    Task<Invoice?> Handle(CreateInvoiceCommand command);
}