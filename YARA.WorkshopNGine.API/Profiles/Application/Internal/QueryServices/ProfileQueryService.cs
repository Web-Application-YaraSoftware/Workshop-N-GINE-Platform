using YARA.WorkshopNGine.API.Profiles.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Profiles.Domain.Model.Queries;
using YARA.WorkshopNGine.API.Profiles.Domain.Repositories;
using YARA.WorkshopNGine.API.Profiles.Domain.Services;

namespace YARA.WorkshopNGine.API.Profiles.Application.Internal.QueryServices;

public class ProfileQueryService(IProfileRepository profileRepository) : IProfileQueryService
{
    public async Task<Profile?> Handle(GetProfileByIdQuery query)
    {
        return await profileRepository.FindByIdAsync(query.ProfileId);
    }

    public async Task<Profile?> Handle(GetProfileByDniQuery query)
    {
        return await profileRepository.FindByDniAsync(query.Dni);
    }

    public async Task<Profile?> Handle(GetProfileByUserIdQuery query)
    {
        return await profileRepository.FindByUserIdAsync(query.UserId);
    }
}