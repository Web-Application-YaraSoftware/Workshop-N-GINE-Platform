using YARA.WorkshopNGine.API.Service.Domain.Model.ValueObjects;
using Task = YARA.WorkshopNGine.API.Service.Domain.Model.Entities.Task;

namespace YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;

public partial class Intervention
{
    public InterventionTypes Type { get; private set; }
    
    public InterventionStatuses Status { get; private set; }
    
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