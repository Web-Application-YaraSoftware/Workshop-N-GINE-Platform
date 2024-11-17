using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Transform;

public class VehicleResourceFromEntityAssembler
{
    public static VehicleResource ToResourceFromEntity(Vehicle entity)
    {
        return new VehicleResource(entity.Id, entity.LicensePlate, entity.Brand, entity.Model, entity.Image, entity.UserId, entity.IoTDeviceId);
    }
}