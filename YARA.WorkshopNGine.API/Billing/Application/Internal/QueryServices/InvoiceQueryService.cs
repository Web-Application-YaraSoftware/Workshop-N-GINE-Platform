using YARA.WorkshopNGine.API.Billing.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Billing.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Billing.Domain.Repositories;
using YARA.WorkshopNGine.API.Billing.Domain.Services;

namespace YARA.WorkshopNGine.API.Billing.Application.Internal.QueryServices;

public class InvoiceQueryService(IInvoiceRepository invoiceRepository) : IInvoiceQueryService
{
    public Task<IEnumerable<Invoice>> Handle(GetAllInvoiceByWorkshopIdQuery query)
    { 
        return invoiceRepository.FindAllByWorkshopIdAsync(query.WorkshopId);
    }
}