namespace YARA.WorkshopNGine.API.Service.Domain.Model.Commands;

public record CreateClientCommand(string FirstName, string LastName, int Dni, string Email, int Age, string Location);