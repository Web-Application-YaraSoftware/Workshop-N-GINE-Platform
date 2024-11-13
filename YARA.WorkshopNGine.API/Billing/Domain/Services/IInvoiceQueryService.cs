using YARA.WorkshopNGine.API.Billing.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Billing.Domain.Model.Queries;

namespace YARA.WorkshopNGine.API.Billing.Domain.Services;

public interface IInvoiceQueryService
{
    Task<IEnumerable<Invoice>> Handle(GetAllInvoiceByWorkshopIdQuery query);
}