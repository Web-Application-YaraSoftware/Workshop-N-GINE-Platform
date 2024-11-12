namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

public record CreateVehicleResource(string LicensePlate, string Brand, string Model, string Image, long UserId, long IoTDeviceId);