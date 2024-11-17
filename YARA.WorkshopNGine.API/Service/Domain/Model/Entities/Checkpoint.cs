using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;

namespace YARA.WorkshopNGine.API.Service.Domain.Model.Entities;

public class Checkpoint
{
    public long Id { get;}
    
    public string Name { get; set; }
    
    public Task Task { get; set; }
    
    public long TaskId { get; set; }

    public Checkpoint()
    {
        this.Name = string.Empty;
    }
    
    public Checkpoint(CreateCheckpointCommand command, long taskId) : this()
    {
        Name = command.Name;
        TaskId = taskId;
    }
    
    public void Update(UpdateCheckpointCommand command)
    {
        Name = command.Name;
    }
}