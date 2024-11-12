namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

public record VehicleResource(long Id, string LicensePlate, string Brand, string Model, string? Image, long UserId, long? IoTDeviceId);