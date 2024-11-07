namespace YARA.WorkshopNGine.API.IAM.Domain.Model.Commands;

public record CreateUserWithRoleMechanicCommand(string Username, string Password, long WorkshopId);