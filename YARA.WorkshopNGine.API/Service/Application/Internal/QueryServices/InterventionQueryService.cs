using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Entities;
using YARA.WorkshopNGine.API.Service.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Service.Domain.Repositories;
using YARA.WorkshopNGine.API.Service.Domain.Services;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;
using Task = YARA.WorkshopNGine.API.Service.Domain.Model.Entities.Task;

namespace YARA.WorkshopNGine.API.Service.Application.Internal.QueryServices;

public class InterventionQueryService(IInterventionRepository interventionRepository, IWorkshopRepository workshopRepository, IUnitOfWork unitOfWork) : IInterventionQueryService
{
    public async Task<Intervention?> Handle(GetInterventionByIdQuery query)
    {
        return await interventionRepository.FindByIdAsync(query.InterventionId);
    }

    public async Task<IEnumerable<Intervention>> Handle(GetAllInterventionsByWorkshopQuery query)
    {
        return await interventionRepository.FindAllByWorkshopIdAsync(query.WorkshopId);
    }

    public async Task<IEnumerable<Intervention>> Handle(GetAllInterventionsByVehicleQuery query)
    {
        return await interventionRepository.FindAllByVehicleIdAsync(query.VehicleId);
    }

    public async Task<IEnumerable<Intervention>> Handle(long workshopId, GetAllInterventionsByWorkshopAndMechanicLeader query)
    {
        if(!workshopRepository.ExistsById(workshopId))
            throw new Exception($"Workshop with the id '{workshopId}' does not exist.");
        return await interventionRepository.FindAllByWorkshopAndMechanicLeaderIdAsync(workshopId, query.MechanicLeaderId);
    }

    public async Task<IEnumerable<Intervention>> Handle(long workshopId, GetAllInterventionsByWorkshopAndMechanicAssistant query)
    {
        if(!workshopRepository.ExistsById(workshopId))
            throw new Exception($"Workshop with the id '{workshopId}' does not exist.");
        var interventions = await interventionRepository.FindAllByWorkshopAndIsNotMechanicLeaderIdAsync(workshopId, query.MechanicAssistantId);
        return interventions.Where(i => i.ExistsAnyTaskByMechanicAssignedId(query.MechanicAssistantId));
    }

    public async Task<IEnumerable<Task>> Handle(long interventionId, GetAllTasksByInterventionQuery query)
    {
        var intervention = await interventionRepository.FindByIdWithTasksAsync(interventionId);
        var tasks = intervention?.Tasks ?? new List<Task>();
        return tasks;
    }

    public async Task<IEnumerable<Task>> Handle(long interventionId, GetAllTasksByInterventionAndMechanicAssignedQuery query)
    {
        var intervention = await interventionRepository.FindByIdWithTasksAsync(interventionId);
        var tasks = intervention?.FindAllTasksByMechanicAssignedId(query.MechanicAssignedId) ?? new List<Task>();
        return tasks;
    }

    public async Task<IEnumerable<Checkpoint>> Handle(long interventionId, long taskId, GetAllCheckpointsByTaskAndInterventionQuery query)
    {
        var intervention = await interventionRepository.FindByIdWithTaskAndCheckpointsAsync(interventionId);
        var task = intervention?.FindTaskById(taskId);
        var checkpoints = task?.Checkpoints ?? new List<Checkpoint>();
        return checkpoints;
    }
}