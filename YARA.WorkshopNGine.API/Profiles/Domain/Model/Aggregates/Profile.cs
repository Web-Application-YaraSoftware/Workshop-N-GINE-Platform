using YARA.WorkshopNGine.API.Profiles.Domain.Model.Commands;

namespace YARA.WorkshopNGine.API.Profiles.Domain.Model.Aggregates;

public class Profile(string firstName, string lastName, int dni, string email, int age, string location, long userId)
{
    public long Id { get; }

    public string FirstName { get; private set; } = firstName;

    public string LastName { get; private set; } = lastName;

    public int Dni { get; private set; } = dni;
    
    public string Email { get; private set; } = email;
    
    public int Age { get; private set; } = age;
    
    public string Location { get; private set; } = location;

    public long UserId { get; private set; } = userId;

    public Profile(CreateProfileCommand command) : this(string.Empty, string.Empty, 0, string.Empty, 0, string.Empty, 0L)
    {
        FirstName = command.FirstName;
        LastName = command.LastName;
        Dni = command.Dni;
        Email = command.Email;
        Age = command.Age;
        Location = command.Location;
        UserId = command.UserId;
    }
    public void UpdateProfileInformation(UpdateProfileCommand command)
    {
        FirstName = command.FirstName;
        LastName = command.LastName;
        Dni = command.Dni;
        Email = command.Email;
        Age = command.Age;
        Location = command.Location;
    }
}