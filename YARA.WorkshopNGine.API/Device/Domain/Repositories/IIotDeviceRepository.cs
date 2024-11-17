using YARA.WorkshopNGine.API.Device.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.Device.Domain.Repositories;

public interface IIotDeviceRepository: IBaseRepository<IotDevice>
{
    Task<IotDevice?> FindByVehicleIdAsync(long vehicleId);
}