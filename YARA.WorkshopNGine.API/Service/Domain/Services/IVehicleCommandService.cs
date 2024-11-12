using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;

namespace YARA.WorkshopNGine.API.Service.Domain.Services;

public interface IVehicleCommandService
{
    Task<Vehicle?> Handle(CreateVehicleCommand command);
    
    Task<Vehicle?> Handle(long vehicleId, UpdateVehicleCommand command);
    
    Task<long?> Handle(DeleteVehicleCommand command);
}