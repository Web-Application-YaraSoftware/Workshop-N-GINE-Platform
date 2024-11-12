using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Domain.Model.ValueObjects;
using Task = YARA.WorkshopNGine.API.Service.Domain.Model.Entities.Task;

namespace YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;

public partial class Intervention
{
    public InterventionTypes Type { get; private set; }
    
    public InterventionStatuses Status { get; private set; }
    
    public DateTime ScheduledAt { get; private set; }
    
    public DateTime? StartedAt { get; private set; }
    
    public DateTime? FinishedAt { get; private set; }
    
    public ICollection<Task> Tasks { get; }

    public Intervention()
    {
        Description = string.Empty;
        Tasks = new List<Task>();
        Status = InterventionStatuses.Pending;
        Type = InterventionTypes.Reparation;
    }
    
    public Task? FindTaskById(long taskId)
    {
        return Tasks.FirstOrDefault(t => t.Id == taskId);
    }
    
    public Task AddTask(CreateTaskCommand command)
    {
        var task = new Task(command, Id);
        Tasks.Add(task);
        return task;
    }
    
    public Task UpdateTask(long taskId, UpdateTaskCommand command)
    {
        var task = FindTaskById(taskId);
        if (task == null)
            throw new InvalidOperationException($"Task with the id '{taskId}' does not exist.");
        task.Update(command);
        return task;
    }
    
    public bool RemoveTask(long taskId)
    {
        var task = FindTaskById(taskId);
        if (task == null)
            return false;
        Tasks.Remove(task);
        return true;
    }
    
    public void Start()
    {
        if (Status != InterventionStatuses.Pending)
            throw new InvalidOperationException("Intervention is not pending.");
        Status = InterventionStatuses.InProgress;
        StartedAt = DateTime.UtcNow;
    }
    
    public void Finish()
    {
        if (Status != InterventionStatuses.InProgress)
            throw new InvalidOperationException("Intervention is not in progress.");
        Status = InterventionStatuses.Completed;
        FinishedAt = DateTime.UtcNow;
    }
    
    public void Cancel()
    {
        if (Status == InterventionStatuses.Completed)
            throw new InvalidOperationException("Intervention is already completed.");
        Status = InterventionStatuses.Canceled;
    }
    
    public ICollection<Task> FindAllTasksByMechanicAssignedId(long mechanicId)
    {
        return Tasks.Where(t => t.MechanicAssignedId == mechanicId).ToList();
    }
    
    public bool ExistsAnyTaskByMechanicAssignedId(long mechanicId)
    {
        return Tasks.Any(t => t.MechanicAssignedId == mechanicId);
    }
    
    public bool IsInProgress()
    {
        return Status == InterventionStatuses.InProgress;
    }
    
    public bool IsAllTasksCompleted()
    {
        return Tasks.All(t => t.Status == TaskStatuses.Completed);
    }
    
    public string StatusToString()
    {
        return Status switch
        {
            InterventionStatuses.Pending => "Pending",
            InterventionStatuses.InProgress => "In Progress",
            InterventionStatuses.Completed => "Completed",
            InterventionStatuses.Canceled => "Canceled",
            _ => "Unknown"
        };
    }
    
    public string TypeToString()
    {
        return Type switch
        {
            InterventionTypes.Reparation => "Reparation",
            InterventionTypes.Maintenance => "Maintenance",
            _ => "Unknown"
        };
    }
}