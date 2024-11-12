using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Service.Domain.Model.ValueObjects;

namespace YARA.WorkshopNGine.API.Service.Domain.Model.Entities;

public partial class Task
{
    public TaskStatuses Status { get; set; }
    
    public ICollection<Checkpoint> Checkpoints { get; }
    
    public Task()
    {
        Description = string.Empty;
        Status = TaskStatuses.Pending;
        Checkpoints = new List<Checkpoint>();
    }
    
    public Checkpoint? FindCheckpointById(long checkpointId)
    {
        return Checkpoints.FirstOrDefault(c => c.Id == checkpointId);
    }
    
    public Checkpoint AddCheckpoint(CreateCheckpointCommand command)
    {
        var checkpoint = new Checkpoint(command, Id);
        Checkpoints.Add(checkpoint);
        return checkpoint;
    }
    
    public Checkpoint UpdateCheckpoint(long checkpointId, UpdateCheckpointCommand command)
    {
        var checkpoint = FindCheckpointById(checkpointId);
        if (checkpoint == null)
            throw new InvalidOperationException($"Checkpoint with the id '{checkpointId}' does not exist.");
        checkpoint.Update(command);
        return checkpoint;
    }
    
    public bool RemoveCheckpoint(long checkpointId)
    {
        var checkpoint = FindCheckpointById(checkpointId);
        if (checkpoint == null)
            return false;
        Checkpoints.Remove(checkpoint);
        return true;
    }
    
    public bool IsInProgress()
    {
        return Status == TaskStatuses.InProgress;
    }
    
    public void Start()
    {
        if (Status != TaskStatuses.Pending)
            throw new InvalidOperationException("Task is not pending.");
        Status = TaskStatuses.InProgress;
    }
    
    public void Complete()
    {
        if (Status != TaskStatuses.InProgress)
            throw new InvalidOperationException("Task is not in progress.");
        Status = TaskStatuses.Completed;
    }
    
    public string StatusToString()
    {
        return Status switch
        {
            TaskStatuses.Pending => "Pending",
            TaskStatuses.InProgress => "In Progress",
            TaskStatuses.Completed => "Completed",
            _ => "Unknown"
        };
    }
}