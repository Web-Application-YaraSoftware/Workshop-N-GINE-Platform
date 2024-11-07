namespace YARA.WorkshopNGine.API.IAM.Domain.Model.Commands;

public record CreateUserWithRoleClientCommand(string Username, string Password, long WorkshopId);