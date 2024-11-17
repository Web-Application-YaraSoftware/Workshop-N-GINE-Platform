using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;

namespace YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;

public class Vehicle
{
    public long Id { get;}
    
    public string LicensePlate { get; private set; }
    
    public string Brand { get; private set; }
    
    public string Model { get; private set; }
    
    public string? Image { get; private set; }
    
    public long UserId { get; private set; }
    
    public long? IoTDeviceId { get; private set; }

    public Vehicle()
    {
        this.LicensePlate = string.Empty;
        this.Brand = string.Empty;
        this.Model = string.Empty;
        this.Image = string.Empty;
    }
    
    public Vehicle(CreateVehicleCommand command) : this()
    {
        LicensePlate = command.LicensePlate;
        Brand = command.Brand;
        Model = command.Model;
        Image = command.Image;
        UserId = command.UserId;
        IoTDeviceId = command.IoTDeviceId == 0 ? null : command.IoTDeviceId;
    }
    
    public void Update(UpdateVehicleCommand command)
    {
        LicensePlate = command.LicensePlate;
        Brand = command.Brand;
        Model = command.Model;
        Image = command.Image;
    }
}