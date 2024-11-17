using YARA.WorkshopNGine.API.Billing.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.Billing.Domain.Repositories;

public interface IInvoiceRepository: IBaseRepository<Invoice>
{
    Task<IEnumerable<Invoice>> FindAllByWorkshopIdAsync(long workshopId);

}