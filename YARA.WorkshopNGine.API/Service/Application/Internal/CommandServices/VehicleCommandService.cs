using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Domain.Repositories;
using YARA.WorkshopNGine.API.Service.Domain.Services;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.Service.Application.Internal.CommandServices;

public class VehicleCommandService(IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork) : IVehicleCommandService
{
    public async Task<Vehicle?> Handle(CreateVehicleCommand command)
    {
        if(vehicleRepository.ExistsByLicensePlate(command.LicensePlate))
            throw new Exception($"Vehicle with license plate {command.LicensePlate} already exists");
        // TODO: Only the owner of the vehicle can create a vehicle or the owner of the workshop can assign a vehicle to a user
        // TODO: Check if the user id is a valid user and their role is a client
        var vehicle = new Vehicle(command);
        try
        {
            await vehicleRepository.AddAsync(vehicle);
            await unitOfWork.CompleteAsync();
            return vehicle;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating a vehicle: {e.Message}");
            throw;
        }
    }

    public async Task<Vehicle?> Handle(long vehicleId, UpdateVehicleCommand command)
    {
        if(vehicleRepository.ExistsByLicensePlate(command.LicensePlate))
            throw new Exception($"Vehicle with license plate {command.LicensePlate} already exists");
        // TODO: Only the owner of the vehicle can update a vehicle or the owner of the workshop can update it as well
        var vehicle = await vehicleRepository.FindByIdAsync(vehicleId);
        if(vehicle == null)
            throw new Exception($"Vehicle with id {vehicleId} not found");
        vehicle.Update(command);
        try
        {
            await unitOfWork.CompleteAsync();
            return vehicle;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while updating a vehicle: {e.Message}");
            throw;
        }
    }

    public async Task<long?> Handle(DeleteVehicleCommand command)
    {
        var vehicle = await vehicleRepository.FindByIdAsync(command.VehicleId);
        if(vehicle == null)
            throw new Exception($"Vehicle with id {command.VehicleId} not found");
        // TODO: Validate it hasn't any interventions in progress
        try
        {
            vehicleRepository.Remove(vehicle);
            await unitOfWork.CompleteAsync();
            return vehicle.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while deleting a vehicle: {e.Message}");
            throw;
        }
    }
}