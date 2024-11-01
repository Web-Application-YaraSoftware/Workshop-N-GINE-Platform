namespace YARA.WorkshopNGine.API.IAM.Domain.Model.Commands;

public record SignUpCommand(string Username, string Password, long RoleId, long WorkshopId);