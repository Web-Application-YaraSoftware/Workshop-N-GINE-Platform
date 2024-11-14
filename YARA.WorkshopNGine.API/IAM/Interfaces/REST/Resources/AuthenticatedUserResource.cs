namespace YARA.WorkshopNGine.API.IAM.Interfaces.REST.Resources;

public record AuthenticatedUserResource(long Id, string Username, long RoleId, long? WorkshopId);