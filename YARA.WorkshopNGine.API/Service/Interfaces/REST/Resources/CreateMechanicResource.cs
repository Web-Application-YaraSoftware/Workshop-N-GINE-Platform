namespace YARA.WorkshopNGine.API.Service.Interfaces.REST.Resources;

public record CreateMechanicResource(string FirstName, string LastName, int Dni, string Email, int Age, string Location);