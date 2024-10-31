using YARA.WorkshopNGine.API.Profiles.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.Shared.Domain.Repositories;

namespace YARA.WorkshopNGine.API.Profiles.Domain.Repositories;

public interface IProfileRepository : IBaseRepository<Profile>
{
    Task<Profile?> FindByDniAsync(int dni);
    
    Task<Profile?> FindByUserIdAsync(long userId);
    
}