using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Domain.Model.Entities;
using YARA.WorkshopNGine.API.Service.Domain.Repositories;
using YARA.WorkshopNGine.API.Service.Domain.Services;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;
using Task = YARA.WorkshopNGine.API.Service.Domain.Model.Entities.Task;

namespace YARA.WorkshopNGine.API.Service.Application.Internal.CommandServices;

public class InterventionCommandService(IInterventionRepository interventionRepository, IWorkshopRepository workshopRepository, IUnitOfWork unitOfWork) : IInterventionCommandService
{
    public async Task<Intervention?> Handle(CreateInterventionCommand command)
    {
        if(!workshopRepository.ExistsById(command.WorkshopId))
            throw new Exception($"Workshop with the id '{command.WorkshopId}' does not exist.");
        // TODO: Only the role owner is able to do this
        if (interventionRepository.ExistsByVehicleIdAndStatusIsPending(command.VehicleId))
            throw new Exception($"There is already an intervention pending for the vehicle with the id '{command.VehicleId}'.");
        // TODO: Only the role owner is able to do this
        if (interventionRepository.ExistsByMechanicLeaderIdAndTimeRange(command.MechanicLeaderId, command.ScheduledDate))
            throw new Exception($"The mechanic leader with the id '{command.MechanicLeaderId}' is already assigned to another intervention in the same time range.");
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
        // TODO: Only the role owner is able to do this
        // TODO: Validate if the vehicle Id exists
        if (interventionRepository.ExistsByVehicleIdAndStatusIsPending(command.VehicleId))
            throw new Exception($"There is already an intervention pending for the vehicle with the id '{command.VehicleId}'.");
        // TODO: Only the role owner is able to do this
        // TODO: Validate if the mechanic leader Id exists
        if (interventionRepository.ExistsByMechanicLeaderIdAndTimeRange(command.MechanicLeaderId, command.ScheduledDate))
            throw new Exception($"The mechanic leader with the id '{command.MechanicLeaderId}' is already assigned to another intervention in the same time range.");
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

    public async Task<Task?> Handle(long interventionId, CreateTaskCommand command)
    {
        var intervention = await interventionRepository.FindByIdAsync(interventionId);
        if (intervention == null)
            throw new Exception($"Intervention with the id '{interventionId}' does not exist.");
        // TODO: Only the mechanic leader is able to do this
        // TODO: Validate if the mechanic assigned id exists
        // TODO: Validate if the mechanic assigned id is available
        if (!intervention.IsInProgress())
            throw new Exception($"Intervention with the id '{interventionId}' is not in progress.");
        var task = intervention.AddTask(command);
        try
        {
            interventionRepository.Update(intervention);
            await unitOfWork.CompleteAsync();
            return task;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the task: {e.Message}");
            return null;
        }
    }

    public async Task<Task?> Handle(long interventionId, long taskId, UpdateTaskCommand command)
    {
        var intervention = await interventionRepository.FindByIdWithTasksAsync(interventionId);
        if (intervention == null)
            throw new Exception($"Intervention with the id '{interventionId}' does not exist.");
        // TODO: Only the mechanic leader is able to do this
        // TODO: Validate if the mechanic assigned id exists
        // TODO: Validate if the mechanic assigned id is available
        if (!intervention.IsInProgress())
            throw new Exception($"Intervention with the id '{interventionId}' is not in progress.");
        var task = intervention.UpdateTask(taskId, command);
        try
        {
            interventionRepository.Update(intervention);
            await unitOfWork.CompleteAsync();
            return task;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while updating the task: {e.Message}");
            return null;
        }
    }

    public async Task<long?> Handle(long interventionId, DeleteTaskCommand command)
    {
        var intervention = await interventionRepository.FindByIdWithTasksAsync(interventionId);
        if (intervention == null)
            throw new Exception($"Intervention with the id '{interventionId}' does not exist.");
        var isRemoved = intervention.RemoveTask(command.TaskId);
        if (!isRemoved)
            return null;
        try
        {
            interventionRepository.Update(intervention);
            await unitOfWork.CompleteAsync();
            return command.TaskId;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while deleting the task: {e.Message}");
            return null;
        }
    }

    public async Task<Checkpoint?> Handle(long interventionId, long taskId, CreateCheckpointCommand command)
    {
        var intervention = await interventionRepository.FindByIdWithTasksAsync(interventionId);
        if (intervention == null)
            throw new Exception($"Intervention with the id '{interventionId}' does not exist.");
        // TODO: Only the mechanic assigned is able to do this
        // TODO: Validate if the mechanic assigned id exists
        // TODO: Validate if the mechanic assigned id is available
        if (!intervention.IsInProgress())
            throw new Exception($"Intervention with the id '{interventionId}' is not in progress.");
        if (!intervention.IsInProgressTask(taskId))
            throw new Exception($"Task with the id '{taskId}' is not in progress.");
        var checkpoint = intervention.AddCheckpoint(taskId, command);
        try
        {
            interventionRepository.Update(intervention);
            await unitOfWork.CompleteAsync();
            return checkpoint;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating the checkpoint: {e.Message}");
            return null;
        }
    }

    public async Task<Checkpoint?> Handle(long interventionId, long taskId, long checkpointId, UpdateCheckpointCommand command)
    {
        var intervention = await interventionRepository.FindByIdWithTaskAndCheckpointsAsync(interventionId);
        if (intervention == null)
            throw new Exception($"Intervention with the id '{interventionId}' does not exist.");
        // TODO: Only the mechanic assigned is able to do this
        // TODO: Validate if the mechanic assigned id exists
        // TODO: Validate if the mechanic assigned id is available
        if (!intervention.IsInProgress())
            throw new Exception($"Intervention with the id '{interventionId}' is not in progress.");
        if (!intervention.IsInProgressTask(taskId))
            throw new Exception($"Task with the id '{taskId}' is not in progress.");
        var checkpoint = intervention.UpdateCheckpoint(taskId, checkpointId, command);
        try
        {
            interventionRepository.Update(intervention);
            await unitOfWork.CompleteAsync();
            return checkpoint;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while updating the checkpoint: {e.Message}");
            return null;
        }
    }

    public async Task<long?> Handle(long interventionId, long taskId, DeleteCheckpointCommand command)
    {
        var intervention = await interventionRepository.FindByIdWithTaskAndCheckpointsAsync(interventionId);
        if (intervention == null)
            throw new Exception($"Intervention with the id '{interventionId}' does not exist.");
        var isRemoved = intervention.RemoveCheckpoint(taskId, command.CheckpointId);
        if (!isRemoved)
            return null;
        try
        {
            interventionRepository.Update(intervention);
            await unitOfWork.CompleteAsync();
            return command.CheckpointId;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while deleting the checkpoint: {e.Message}");
            return null;
        }
    }

    public async Task<long?> Handle(InProgressInterventionCommand command)
    {
        var intervention = await interventionRepository.FindByIdAsync(command.InterventionId);
        if (intervention == null)
            throw new Exception($"Intervention with the id '{command.InterventionId}' does not exist.");
        intervention.Start();
        try
        {
            interventionRepository.Update(intervention);
            await unitOfWork.CompleteAsync();
            return command.InterventionId;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while starting the intervention: {e.Message}");
            return null;
        }
    }

    public async Task<long?> Handle(CompleteInterventionCommand command)
    {
        var intervention = await interventionRepository.FindByIdAsync(command.InterventionId);
        if (intervention == null)
            throw new Exception($"Intervention with the id '{command.InterventionId}' does not exist.");
        if (!intervention.IsInProgress())
            throw new Exception($"Intervention with the id '{command.InterventionId}' is not in progress.");
        if(!intervention.IsAllTasksCompleted())
            throw new Exception($"Intervention with the id '{command.InterventionId}' has pending tasks.");
        intervention.Finish();
        try
        {
            interventionRepository.Update(intervention);
            await unitOfWork.CompleteAsync();
            return command.InterventionId;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while completing the intervention: {e.Message}");
            return null;
        }
    }

    public async Task<long?> Handle(CancelInterventionCommand command)
    {
        var intervention = await interventionRepository.FindByIdAsync(command.InterventionId);
        if (intervention == null)
            throw new Exception($"Intervention with the id '{command.InterventionId}' does not exist.");
        intervention.Cancel();
        try
        {
            interventionRepository.Update(intervention);
            await unitOfWork.CompleteAsync();
            return command.InterventionId;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while canceling the intervention: {e.Message}");
            return null;
        }
    }
}