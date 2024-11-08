using YARA.WorkshopNGine.API.Profiles.interfaces.ACL;
using YARA.WorkshopNGine.API.Service.Domain.Model.ValueObjects;

namespace YARA.WorkshopNGine.API.Service.Application.Internal.OutboundServices.ACL;

public class ExternalProfileService(IProfilesContextFacade profilesContextFacade)
{
    public async Task<ProfileId?> CreateProfileAsync(string firstName, string lastName, int dni, string email, int age, string location, long userId)
    {
        var profileId = await profilesContextFacade.CreateProfile(firstName, lastName, dni, email, age, location, userId);
        if (profileId == 0) return await Task.FromResult<ProfileId?>(null);
        return new ProfileId(profileId);
    } 
}