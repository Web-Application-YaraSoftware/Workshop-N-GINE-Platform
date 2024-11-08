using Microsoft.EntityFrameworkCore;
using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Repositories;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace YARA.WorkshopNGine.API.Service.Infrastructure.Persistence.EFC.Repositories;

public class InterventionRepository(AppDbContext context) : BaseRepository<Intervention> (context), IInterventionRepository
{
    public async Task<IEnumerable<Intervention>> FindAllByWorkshopIdAsync(long workshopId)
    {
        return await Context.Set<Intervention>().Where(i => i.WorkshopId == workshopId).ToListAsync();
    }
}