using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.Service.Domain.Repositories;

public interface IInterventionRepository : IBaseRepository<Intervention>
{
    Task<IEnumerable<Intervention>> FindAllByWorkshopIdAsync(long workshopId);
    
    bool ExistsById(long id);
    
    Task<Intervention?> FindByIdWithTasksAsync(long id);
    
    bool ExistsByVehicleIdAndStatusIsPending(long vehicleId);
    
    bool ExistsByMechanicLeaderIdAndTimeRange(long mechanicLeaderId, DateTime scheduledAt);
}