using YARA.WorkshopNGine.API.Device.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Device.Domain.Model.Queries;

namespace YARA.WorkshopNGine.API.Device.Domain.Services;

public interface IIotDeviceQueryService
{
    Task<IotDevice?> Handle(GetIotDeviceByVehicleIdQuery query);
}