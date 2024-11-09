using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;

namespace YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;

public partial class Intervention
{
    public long Id { get;}
    
    public long WorkshopId { get; private set; }
    
    public long VehicleId { get; private set; }
    
    public long MechanicLeaderId { get; private set; }
    
    public string Description { get; private set; }
    
    public Intervention(long workshopId, long vehicleId, long mechanicLeaderId, string description): this()
    {
        WorkshopId = workshopId;
        VehicleId = vehicleId;
        MechanicLeaderId = mechanicLeaderId;
        Description = description;
    }

    public Intervention(CreateInterventionCommand command) : this()
    {
        WorkshopId = command.WorkshopId;
        VehicleId = command.VehicleId;
        MechanicLeaderId = command.MechanicLeaderId;
        Description = command.Description;
        ScheduledAt = command.ScheduledDate;
    }
    
    public void Update(UpdateInterventionCommand command)
    {
        VehicleId = command.VehicleId;
        MechanicLeaderId = command.MechanicLeaderId;
        Description = command.Description;
        Type = command.Type;
        ScheduledAt = command.ScheduledDate;
    }
}