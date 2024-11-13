using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Entities;
using YARA.WorkshopNGine.API.Subscription.Domain.Repositories;

namespace YARA.WorkshopNGine.API.Subscription.Infrastructure.Persistence.EFC.Repositories;

public class PlanRepository(AppDbContext context) : BaseRepository<Plan>(context), IPlanRepository
{
    
}