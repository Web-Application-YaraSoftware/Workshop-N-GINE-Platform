using Microsoft.EntityFrameworkCore;
using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.Service.Domain.Repositories;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using YARA.WorkshopNGine.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Task = YARA.WorkshopNGine.API.Service.Domain.Model.Entities.Task;

namespace YARA.WorkshopNGine.API.Service.Infrastructure.Persistence.EFC.Repositories;

public class InterventionRepository(AppDbContext context) : BaseRepository<Intervention> (context), IInterventionRepository
{
    public async Task<IEnumerable<Intervention>> FindAllByWorkshopIdAsync(long workshopId)
    {
        return await Context.Set<Intervention>().Where(i => i.WorkshopId == workshopId).ToListAsync();
    }

    public async Task<IEnumerable<Intervention>> FindAllByWorkshopAndMechanicLeaderIdAsync(long workshopId, long mechanicLeaderId)
    {
        return await Context.Set<Intervention>().Where(i => i.WorkshopId == workshopId && i.MechanicLeaderId == mechanicLeaderId).ToListAsync();
    }

    public async Task<IEnumerable<Intervention>> FindAllByWorkshopAndIsNotMechanicLeaderIdAsync(long workshopId, long mechanicLeaderId)
    {
        return await Context.Set<Intervention>()
            .Where(i => i.WorkshopId == workshopId && i.MechanicLeaderId != mechanicLeaderId)
            .Include(i => i.Tasks)
            .ToListAsync();
    }

    public bool ExistsById(long id)
    {
        return Context.Set<Intervention>().Any(i => i.Id == id);
    }

    public async Task<Intervention?> FindByIdWithTasksAsync(long id)
    {
        return await Context.Set<Intervention>()
            .Include(i => i.Tasks)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public bool ExistsByVehicleIdAndStatusIsPending(long vehicleId)
    {
        return Context.Set<Intervention>().Any(i => i.VehicleId == vehicleId && i.Status == InterventionStatuses.Pending);
    }

    public bool ExistsByMechanicLeaderIdAndTimeRange(long mechanicLeaderId, DateTime scheduledAt)
    {
        var start = scheduledAt;
        var end = scheduledAt.AddHours(6);
        return Context.Set<Intervention>().Any(i => i.MechanicLeaderId == mechanicLeaderId && i.ScheduledAt >= start && i.ScheduledAt <= end);
    }
}