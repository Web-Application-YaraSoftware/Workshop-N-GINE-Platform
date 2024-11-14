using Microsoft.EntityFrameworkCore;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Entities;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.Subscription.Domain.Repositories;

namespace YARA.WorkshopNGine.API.Subscription.Infrastructure.Persistence.EFC.Repositories;

public class PlanRepository(AppDbContext context) : BaseRepository<Plan>(context), IPlanRepository
{
    public bool ExistsByIdAsync(long id)
    {
        return Context.Set<Plan>().Any(p => p.Id == id);
    }

    public async Task<Plan?> FindByTypeIsBasicAsync()
    {
        return await Context.Set<Plan>().Where(p => p.Type == PlanTypes.Basic).FirstOrDefaultAsync();
    }

    public async Task<Plan?> FindByTypeIsStandardAsync()
    {
        return await Context.Set<Plan>().Where(p => p.Type == PlanTypes.Standard).FirstOrDefaultAsync();
    }

    public async Task<Plan?> FindByTypeIsPremiumAsync()
    {
        return await Context.Set<Plan>().Where(p => p.Type == PlanTypes.Premium).FirstOrDefaultAsync();
    }
}