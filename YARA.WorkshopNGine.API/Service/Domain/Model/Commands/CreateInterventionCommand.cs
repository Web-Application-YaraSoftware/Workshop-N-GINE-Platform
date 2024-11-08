namespace YARA.WorkshopNGine.API.Service.Domain.Model.Commands;

public record CreateInterventionCommand(long WorkshopId, long VehicleId, long MechanicLeaderId, string Description);