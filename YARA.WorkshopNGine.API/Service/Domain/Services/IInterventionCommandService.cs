using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Domain.Model.Entities;
using Task = YARA.WorkshopNGine.API.Service.Domain.Model.Entities.Task;

namespace YARA.WorkshopNGine.API.Service.Domain.Services;

public interface IInterventionCommandService
{
    Task<Intervention?> Handle(CreateInterventionCommand command);
    
    Task<Intervention?> Handle(long interventionId, UpdateInterventionCommand command);
    
    Task<Task?> Handle(long interventionId, CreateTaskCommand command);
    
    Task<Task?> Handle(long interventionId, long taskId, UpdateTaskCommand command);
    
    Task<long?> Handle(long interventionId, DeleteTaskCommand command);

    Task<Checkpoint?> Handle(long interventionId, long taskId, CreateCheckpointCommand command);
    
    Task<Checkpoint?> Handle(long interventionId, long taskId, long checkpointId, UpdateCheckpointCommand command);
    
    Task<long?> Handle(long interventionId, long taskId, DeleteCheckpointCommand command);
    
    Task<long?> Handle(InProgressInterventionCommand command);
    
    Task<long?> Handle(CompleteInterventionCommand command);
    
    Task<long?> Handle(CancelInterventionCommand command);
}