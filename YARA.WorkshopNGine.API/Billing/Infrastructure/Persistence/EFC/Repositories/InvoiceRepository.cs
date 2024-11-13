using Microsoft.EntityFrameworkCore;
using YARA.WorkshopNGine.API.Billing.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Billing.Domain.Repositories;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace YARA.WorkshopNGine.API.Billing.Infrastructure.Persistence.EFC.Repositories;

public class InvoiceRepository(AppDbContext context) : BaseRepository<Invoice> (context), IInvoiceRepository
{
    public async Task<IEnumerable<Invoice>> FindAllByWorkshopIdAsync(long workshopId)
    {
        return await Context.Set<Invoice>().Where(x => x.WorkshopId == workshopId).ToListAsync();
    }
}