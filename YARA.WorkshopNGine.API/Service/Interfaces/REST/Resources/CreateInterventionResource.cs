namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

public record CreateInterventionResource(long WorkshopId, long VehicleId, long MechanicLeaderId, string Description);