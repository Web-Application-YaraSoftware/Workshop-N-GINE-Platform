namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

public record TaskResource(long Id, long MechanicAssignedId, string Description, string Status, long InterventionId);