using YARA.WorkshopNGine.API.Device.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Device.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Device.Interfaces.REST.Transform;

public class IotDeviceResourceFromEntityAssembler
{
    public static IotDeviceResource ToResourceFromEntity(IotDevice entity)
    {
        return new IotDeviceResource(entity.Id, entity.CodeList, entity.VehicleId);
    }
}