using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Entities;
using YARA.WorkshopNGine.API.Service.Domain.Model.Queries;
using Task = YARA.WorkshopNGine.API.Service.Domain.Model.Entities.Task;

namespace YARA.WorkshopNGine.API.Service.Domain.Services;

public interface IInterventionQueryService
{
    Task<Intervention?> Handle(GetInterventionByIdQuery query);
    
    Task<IEnumerable<Intervention>> Handle(GetAllInterventionsByWorkshopQuery query);
    
    Task<IEnumerable<Intervention>> Handle(long workshopId, GetAllInterventionsByWorkshopAndMechanicLeader query);
    
    Task<IEnumerable<Intervention>> Handle(long workshopId, GetAllInterventionsByWorkshopAndMechanicAssistant query);
    
    Task<IEnumerable<Task>> Handle(long interventionId, GetAllTasksByInterventionQuery query);
    
    Task<IEnumerable<Task>> Handle(long interventionId, GetAllTasksByInterventionAndMechanicAssignedQuery query);
    
    Task<IEnumerable<Checkpoint>> Handle(long interventionId, long taskId, GetAllCheckpointsByTaskAndInterventionQuery query);
}