using YARA.WorkshopNGine.API.Service.Domain.Model.ValueObjects;

namespace YARA.WorkshopNGine.API.Service.Domain.Model.Entities;

public partial class Task
{
    public TaskStatuses Status { get; set; }
    
    //TODO: Add List Checkpoints field
    
    public Task()
    {
        Description = string.Empty;
        Status = TaskStatuses.Pending;
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