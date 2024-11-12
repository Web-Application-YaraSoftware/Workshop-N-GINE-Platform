namespace YARA.WorkshopNGine.API.Service.Domain.Model.Commands;

public record UpdateVehicleCommand(string LicensePlate, string Brand, string Model, string Image);