namespace YARA.WorkshopNGine.API.Service.Domain.Model.Commands;

public record CreateVehicleCommand(string LicensePlate, string Brand, string Model, string Image, long UserId, long IoTDeviceId);