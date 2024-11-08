namespace YARA.WorkshopNGine.API.Service.Domain.Model.Commands;

public record CreateMechanicCommand(string FirstName, string LastName, int Dni, string Email, int Age, string Location);