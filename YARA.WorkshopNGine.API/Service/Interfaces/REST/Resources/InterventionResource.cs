namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

public record InterventionResource(long Id, long VehicleId, long MechanicLeaderId, string Description, string Type, string Status, DateTime? StartedAt, DateTime? FinishedAt);