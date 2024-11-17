using YARA.WorkshopNGine.API.Device.Domain.Model.Enitites;

namespace YARA.WorkshopNGine.API.Device.Interfaces.REST.Resources;

public record IotDeviceResource(long Id, List<Code> CodeList, long VehicleId);