using YARA.WorkshopNGine.API.Profiles.Domain.Model.Commands;
using YARA.WorkshopNGine.API.Profiles.Domain.Services;

namespace YARA.WorkshopNGine.API.Profiles.interfaces.ACL.Services;

public class ProfilesContextFacade(IProfileCommandService profileCommandService) : IProfilesContextFacade
{
    public async Task<long> CreateProfile(string firstName, string lastName, int dni, string email, int age, string location, long userId)
    {
        var createProfileCommand = new CreateProfileCommand(firstName, lastName, dni, email, age, location, userId);
        var profile = await profileCommandService.Handle(createProfileCommand);
        return profile?.Id ?? 0;
    }

    public async Task<long> UpdateProfile(long id, string firstName, string lastName, int dni, string email, int age, string location, int userId)
    {
        var updateProfileCommand = new UpdateProfileCommand(id,firstName, lastName, dni, email, age, location, userId);
        var profile = await profileCommandService.Handle(id, updateProfileCommand);
        return profile?.Id ?? 0;
    }
}