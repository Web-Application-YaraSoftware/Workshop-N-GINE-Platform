using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Repositories;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace YARA.WorkshopNGine.API.Service.Infrastructure.Persistence.EFC.Repositories;

public class WorkshopRepository(AppDbContext context) : BaseRepository<Workshop> (context), IWorkshopRepository
{
    public bool ExistsByName(string name)
    {
        return Context.Set<Workshop>().Any(workshop => workshop.Name.Equals(name));
    }
}