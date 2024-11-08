namespace YARA.WorkshopNGine.API.Service.Domain.Model.Commands;

public record CreateTaskCommand(long MechanicAssignedId, string Description, long InterventionId);