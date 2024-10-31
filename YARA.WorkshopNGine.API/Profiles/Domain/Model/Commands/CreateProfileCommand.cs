namespace YARA.WorkshopNGine.API.Profiles.Domain.Model.Commands;

public record CreateProfileCommand(string FirstName, string LastName, int Dni, string Email, int Age, string Location, long UserId);