using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Domain.Repositories;
using YARA.WorkshopNGine.API.Service.Domain.Services;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.Service.Application.Internal.CommandServices;

public class WorkshopCommandService(IWorkshopRepository workshopRepository, IUnitOfWork unitOfWork): IWorkshopCommandService
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
}