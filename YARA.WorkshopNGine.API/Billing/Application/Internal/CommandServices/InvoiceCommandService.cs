using YARA.WorkshopNGine.API.Billing.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Billing.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Billing.Domain.Repositories;
using YARA.WorkshopNGine.API.Billing.Domain.Services;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.Billing.Application.Internal.CommandServices;

public class InvoiceCommandService(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork) : IInvoiceCommandService
{
    public async Task<Invoice?> Handle(CreateInvoiceCommand command)
    {
        /**
         * TODO: Create a facade for Amount, SubscriptionId, and PlanId, from the subscription bounded context
         * and WorkshopId from the workshop in the service.
         */
        var invoice = new Invoice(command);
        try
        {
            await invoiceRepository.AddAsync(invoice);
            await unitOfWork.CompleteAsync();
            return invoice;
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while creating the invoice:", e);
        }
    }
}