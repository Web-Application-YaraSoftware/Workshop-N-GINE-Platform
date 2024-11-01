namespace YARA.WorkshopNGine.API.IAM.Domain.Model.Queries;

public record GetAllUsersByRoleAndWorkshopQuery(long RoleId, long WorkshopId);