using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Domain.Repositories;
using YARA.WorkshopNGine.API.Service.Domain.Services;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.Service.Application.Internal.CommandServices;

public class InterventionCommandService(IInterventionRepository interventionRepository, IWorkshopRepository workshopRepository, IUnitOfWork unitOfWork) : IInterventionCommandService
{
    public async Task<Intervention?> Handle(CreateInterventionCommand command)
    {
        if(!workshopRepository.ExistsById(command.WorkshopId))
            throw new Exception($"Workshop with the id '{command.WorkshopId}' does not exist.");
        // TODO: Validate if the vehicle id exists and it's available and depends on the role of the user
        // TODO: Validate if the mechanic leader id exists and it's available and depends on the role of the user
        var intervention = new Intervention(command);
        try
        {
            await interventionRepository.AddAsync(intervention);
            await unitOfWork.CompleteAsync();
            return intervention;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the intervention: {e.Message}");
            return null;
        }
    }

    public async Task<Intervention?> Handle(long interventionId, UpdateInterventionCommand command)
    {
        var intervention = await interventionRepository.FindByIdAsync(interventionId);
        if (intervention == null)
            throw new Exception($"Intervention with the id '{interventionId}' does not exist.");
        // TODO: Validate if the vehicle id exists and it's available and depends on the role of the user
        // TODO: Validate if the mechanic leader id exists and it's available and depends on the role of the user
        intervention.Update(command);
        try
        {
            interventionRepository.Update(intervention);
            await unitOfWork.CompleteAsync();
            return intervention;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while updating the intervention: {e.Message}");
            return null;
        }
    }
}