using YARA.WorkshopNGine.API.Service.Application.Internal.OutboundServices.ACL;
using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Domain.Model.ValueObjects;
using YARA.WorkshopNGine.API.Service.Domain.Repositories;
using YARA.WorkshopNGine.API.Service.Domain.Services;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.Service.Application.Internal.CommandServices;

public class WorkshopCommandService(IWorkshopRepository workshopRepository, IUnitOfWork unitOfWork, ExternalIamService externalIamService, ExternalProfileService externalProfileService): IWorkshopCommandService
{
    public async Task<Workshop?> Handle(CreateWorkshopCommand command)
    {
        if (workshopRepository.ExistsByName(command.Name))
            throw new Exception($"Workshop with the name '{command.Name}' already exists.");
        var workshop = new Workshop(command);
        try
        {
            await workshopRepository.AddAsync(workshop);
            await unitOfWork.CompleteAsync();
            return workshop;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the workshop: {e.Message}");
            return null;
        }
    }

    public async Task<Workshop?> Handle(long workshopId, UpdateWorkshopCommand command)
    {
        var workshop = await workshopRepository.FindByIdAsync(workshopId);
        if (workshop == null)
        {
            Console.WriteLine("Workshop not found.");
            return null;
        }

        if (workshopRepository.ExistsByName(command.Name))
            throw new Exception($"Workshop with the name '{command.Name}' already exists.");
        workshop.UpdateWorkshopInformation(command);
        try
        {
            workshopRepository.Update(workshop);
            await unitOfWork.CompleteAsync();
            return workshop;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while updating the workshop: {e.Message}");
            return null;
        }
    }

    public async Task<UserId?> Handle(long workshopId, CreateClientCommand command)
    {
        if(!workshopRepository.ExistsById(workshopId))
            throw new Exception($"Workshop with the id '{workshopId}' does not exist.");
        var userId = await externalIamService.CreateUserWithRoleClientAsync(command.Email, command.Dni.ToString(), workshopId);
        if (userId == null)
            throw new Exception("An error occurred while creating the user.");
        // TODO: If the creation of the profile fails, the user should be deleted.
        var profileId = await externalProfileService.CreateProfileAsync(command.FirstName, command.LastName, command.Dni, command.Email, command.Age, command.Location, userId.Id);
        if (profileId == null)
            throw new Exception("An error occurred while creating the profile.");
        return userId;
    }

    public async Task<UserId?> Handle(long workshopId, CreateMechanicCommand command)
    {
        if(!workshopRepository.ExistsById(workshopId))
            throw new Exception($"Workshop with the id '{workshopId}' does not exist.");
        var userId = await externalIamService.CreateUserWithRoleMechanicAsync(command.Email, command.Dni.ToString(), workshopId);
        if (userId == null)
            throw new Exception("An error occurred while creating the user.");
        // TODO: If the creation of the profile fails, the user should be deleted.
        var profileId = await externalProfileService.CreateProfileAsync(command.FirstName, command.LastName, command.Dni, command.Email, command.Age, command.Location, userId.Id);
        if (profileId == null)
            throw new Exception("An error occurred while creating the profile.");
        return userId;
    }
}