using YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;

namespace YARA.WorkshopNGine.API.Service.Domain.Model.Entities;

public partial class Task
{
    public long Id { get;}
    
    public long MechanicAssignedId { get; set; }
    
    public string Description { get; set; }
    
    public Intervention Intervention { get; set; }
    
    public long InterventionId { get; set; }
    
    public Task(long mechanicAssignedId, string description, long interventionId) : this()
    {
        MechanicAssignedId = mechanicAssignedId;
        Description = description;
        InterventionId = interventionId;
    }

    public Task(CreateTaskCommand command, long interventionId) : this()
    {
        MechanicAssignedId = command.MechanicAssignedId;
        Description = command.Description;
        InterventionId = interventionId;
    }
    
    public void Update(UpdateTaskCommand command)
    {
        MechanicAssignedId = command.MechanicAssignedId;
        Description = command.Description;
    }
}