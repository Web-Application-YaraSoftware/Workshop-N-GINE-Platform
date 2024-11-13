using Microsoft.EntityFrameworkCore;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Subscription.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.Subscription.Domain.Repositories;

namespace YARA.WorkshopNGine.API.Subscription.Infrastructure.Persistence.EFC.Repositories;

public class SubscriptionItemRepository(AppDbContext context) : BaseRepository<SubscriptionItem>(context), ISubscriptionItemRepository
{
    public async Task<SubscriptionItem?> FindByWorkshopIdAndStatusIsActiveAsync(long workshopId)
    {
        return await Context.Set<SubscriptionItem>().Where(s => s.WorkshopId.Value == workshopId && s.Status == SubscriptionStatuses.Active).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<SubscriptionItem>> FindAllByWorkshopIdAsync(long workshopId)
    {
        return await Context.Set<SubscriptionItem>().Where(s => s.WorkshopId.Value == workshopId).ToListAsync();
    }

    public bool ExistsByWorkshopIdAndStatusIsActive(long workshopId)
    {
        return Context.Set<SubscriptionItem>().Any(s => s.WorkshopId.Value == workshopId && s.Status == SubscriptionStatuses.Active);
    }
}