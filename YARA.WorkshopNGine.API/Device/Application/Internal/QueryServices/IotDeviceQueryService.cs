using YARA.WorkshopNGine.API.Device.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Device.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Device.Domain.Repositories;
using YARA.WorkshopNGine.API.Device.Domain.Services;

namespace YARA.WorkshopNGine.API.Device.Application.Internal.QueryServices;

public class IotDeviceQueryService(IIotDeviceRepository iotDeviceRepository) : IIotDeviceQueryService
{
    public Task<IotDevice?> Handle(GetIotDeviceByVehicleIdQuery query)
    {
        return iotDeviceRepository.FindByVehicleIdAsync(query.VehicleId);
    }
}