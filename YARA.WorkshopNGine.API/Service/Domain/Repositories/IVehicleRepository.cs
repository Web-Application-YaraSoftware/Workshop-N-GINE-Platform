using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.Service.Domain.Repositories;

public interface IVehicleRepository : IBaseRepository<Vehicle>
{
    Task<IEnumerable<Vehicle>> FindAllByUserIdAsync(long userId);
    
    bool ExistsByLicensePlate(string licensePlate);
}