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
}