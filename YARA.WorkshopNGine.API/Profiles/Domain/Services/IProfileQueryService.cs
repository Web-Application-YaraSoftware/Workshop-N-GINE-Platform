using YARA.WorkshopNGine.API.Profiles.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Profiles.Domain.Model.Queries;

namespace YARA.WorkshopNGine.API.Profiles.Domain.Services;

public interface IProfileQueryService
{
    Task<Profile?> Handle(GetProfileByIdQuery query);
    Task<Profile?> Handle(GetProfileByDniQuery query);
    Task<Profile?> Handle(GetProfileByUserIdQuery query);
}