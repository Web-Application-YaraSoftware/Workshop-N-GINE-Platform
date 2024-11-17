namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

public record UpdateInterventionResource(long VehicleId, long MechanicLeaderId, string Description, int Type, DateTime ScheduledAt);